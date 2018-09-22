using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace Fudge.Framework.Database {
    public partial class Contest {
        FudgeDataContext db = new FudgeDataContext();

        public bool HasStarted {
            get {
                return SqlMethods.DateDiffSecond(StartTime, DateTime.UtcNow) > 0;
            }
        }

        public bool HasEnded {
            get {
                return SqlMethods.DateDiffSecond(EndTime, DateTime.UtcNow) > 0;
            }
        }

        public bool IsRunning {
            get {
                return HasStarted && !HasEnded;
            }
        }

        public TimeSpan Duration {
            get {
                return EndTime.Subtract(StartTime);
            }
        }

        public static Contest GetContestById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Contests.SingleOrDefault(c => c.ContestId == id);
        }

        public bool IsScoringSet(ContestScoring scoring) {
            return (Scoring & scoring) == scoring;
        }

        public void SetScoring(ContestScoring scoring) {
            Scoring |= scoring;
            db.SubmitChanges();
        }

        public void UnSetScoring(ContestScoring scoring) {
            Scoring &= ~scoring;
            db.SubmitChanges();
        }

        public void UpdatePoints() {
            FudgeDataContext db = new FudgeDataContext();

            foreach (Run run in Runs) {
                run.UpdatePoints();
            }            

            var contestUsers = db.ContestUsers.Where(cu => cu.ContestId == ContestId);

            foreach (ContestUser contestUser in contestUsers) {
                contestUser.Points = db.Runs.Where(r => r.ContestId == ContestId && r.UserId == contestUser.UserId).Sum(r => r.Points) ?? 0;                
            }

            var rankedContestUsers = GetRankingsList(contestUsers, c => c.Points ?? 0);

            foreach(RankTuple<ContestUser> rankedContestUser in rankedContestUsers) {
                rankedContestUser.Item.Rank = rankedContestUser.Rank;                
            }

            db.SubmitChanges();
        }

        public class RankTuple<T> {
            public int Rank { get; set; }
            public T Item { get; set; }
        }

        // TODO: Move me
        public static IEnumerable<RankTuple<T>> GetRankingsList<T>(IEnumerable<T> initial, Func<T, int> selector) where T : class {
            var ranks = initial.OrderByDescending(selector).ToArray();
            List<RankTuple<T>> ranked = new List<RankTuple<T>>();
            int tieRank = 0, rank = 1;

            for (int i = 0; i < ranks.Length; ++i) {
                if (i > 0 && selector(ranks[i - 1]) == selector(ranks[i])) {
                    tieRank++;
                    ranked.Add(new RankTuple<T> { Item = ranks[i], Rank = ranked.Last().Rank });
                }
                else {
                    rank += tieRank;
                    ranked.Add(new RankTuple<T> { Item = ranks[i], Rank = rank++ });
                    tieRank = 0;
                }
            }

            return ranked;
        }
    }
}
