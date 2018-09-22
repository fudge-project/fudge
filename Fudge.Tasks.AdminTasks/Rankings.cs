using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.AdminTasks {
    public static class Rankings {

        public class RankTuple<T> {
            public int Rank { get; set; }
            public T Item { get; set; }
        }

        public static void Update(DateTime timestamp, bool updateStandings) {
            FudgeDataContext db = new FudgeDataContext();

            double runAvgPopularity = GetAveragePopularity(db.Runs);
            double postAvgPopularity = GetAveragePopularity(db.Posts);

            foreach (User user in db.Users) {
                double userPoints = 0.0;

                userPoints += GetPoints(user.GoodRuns, runAvgPopularity);
                userPoints += GetPoints(user.Posts.Where(p => p.Rating.Count > 0), postAvgPopularity);
                userPoints += user.ReferralCount;

                user.Points = (int)Math.Round(userPoints);
            }

            try {
                foreach (Entity entity in db.Entities) {
                    var users = db.Users.Where(u => u.Affiliation.EntityId == entity.EntityId);
                    entity.Points = users.Any() ? users.Sum(s => s.Points) : 0;
                }

                UpdateGlobalRanks(db);
                UpdateEntityRanks(db);

                if (updateStandings) {
                    Console.WriteLine("[standings] updating standings table");

                    foreach (User user in db.Users) {
                        db.Standings.InsertOnSubmit(new Standing {
                            User = user,
                            Points = user.Points,
                            Timestamp = timestamp,
                            Rank = user.GlobalRank
                        });
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            db.SubmitChanges();
        }

        private static void UpdateGlobalRanks(FudgeDataContext db) {
            var rankedUsers = GetRankingsList(db.Users, u => u.Points);
            var rankedEntities = GetRankingsList(db.Entities, e => e.Points);

            foreach (var r in rankedUsers) {
                r.Item.GlobalRank = r.Rank;
            }

            foreach (var r in rankedEntities) {
                r.Item.GlobalRank = r.Rank;
            }
        }

        private static void UpdateEntityRanks(FudgeDataContext db) {

            foreach (var e in db.Entities) {
                var rankedUsers = GetRankingsList(db.Users.Where(u => u.Affiliation.Entity == e), u => u.Points);

                foreach (var r in rankedUsers) {
                    r.Item.EntityRank = r.Rank;
                }
            }

            foreach (var c in db.Countries) {
                var rankedUsers = GetRankingsList(c.Users, u => u.Points);

                foreach (var r in rankedUsers) {
                    r.Item.CountryRank = r.Rank;
                }
            }
        }

        private static double GetAveragePopularity(IEnumerable rateables) {
            double sum = 0;
            double count = 0;

            foreach (IRateable rateable in rateables) {
                if (rateable.Rating != null) {
                    if (rateable.Rating.Count != 0) {
                        sum += rateable.Rating.Popularity;
                        count++;
                    }
                }
            }

            return sum / count;
        }

        private static double GetPoints(IEnumerable rateables, IEnumerable pool) {
            return GetPoints(rateables, GetAveragePopularity(pool));
        }

        private static double GetPoints(IEnumerable rateables, double avgPopularity) {
            double points = 0;

            foreach (IRateable rateable in rateables) {
                points += GetPoints(rateable, avgPopularity);
            }

            return points;
        }

        private static double GetPoints(IRateable rateable, double avgPopularity) {
            double timeFactor = Math.Pow(0.99, (DateTime.UtcNow - rateable.Timestamp).Days);

            if (rateable.Rating.Count == 0) {
                return timeFactor * rateable.AvgPoints;
            }

            return timeFactor * rateable.AvgPoints * (rateable.Rating.Popularity / avgPopularity);
        }

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
