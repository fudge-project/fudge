using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Collections;

namespace Fudge.Modules.Standings {
    public static class Rankings {

        public class RankTuple<T> {
            public int Rank { get; set; }
            public T Item { get; set; }
        }

        public static void Update(DateTime timestamp, bool updateStandings) {
            FudgeDataContext db = new FudgeDataContext(@"Data Source=163.118.202.41,81;Initial Catalog=fudge;Persist Security Info=True;User ID=webuser;Password=webpassword");

            double runAvgPopularity = GetAveragePopularity(db.Runs);
            double postAvgPopularity = GetAveragePopularity(db.Posts);

            foreach (User user in db.Users) {
                double userPoints = 0.0;

                userPoints += GetPoints(user.GoodRuns, runAvgPopularity);
                userPoints += GetPoints(user.Posts.Where(p => p.Rating.Count > 0), postAvgPopularity);
                userPoints += user.ReferralCount;

                user.Points = (int)Math.Round(userPoints);

                if (updateStandings) {
                    db.Standings.InsertOnSubmit(new Standing { User = user, Points = user.Points, Timestamp = timestamp });
                }
            }

            UpdateGlobalRanks(db);
            UpdateSchoolRanks(db);
            UpdateRegionRanks(db);

            db.SubmitChanges();
        }

        private static void UpdateGlobalRanks(FudgeDataContext db) {
            var globalUserRank = GetRankingsList(db.Users, u => u.Points);
            var globalSchoolRank = GetRankingsList(db.Schools, s => s.Points);
            var globalRegionRank = GetRankingsList(db.Regions, r =>
                r.Schools.Any() ?
                Convert.ToInt32(r.Schools.Average(s => s.Points))
                : 0);
            
            foreach (var r in globalUserRank) {
                r.Item.GlobalRank = r.Rank;
            }

            foreach (var r in globalSchoolRank) {
                r.Item.GlobalRank = r.Rank;
            }

            foreach (var r in globalRegionRank) {
                r.Item.GlobalRank = r.Rank;
            }
        }

        private static void UpdateRegionRanks(FudgeDataContext db) {
            foreach (var region in db.Regions) {
                
                var users = from s in region.Schools
                            from u in s.Users
                            select u;

                var userRankings = GetRankingsList(users, u => u.Points);
                
                foreach (var r in userRankings) {
                    r.Item.RegionRank = r.Rank;
                }
            }
        }

        private static void UpdateSchoolRanks(FudgeDataContext db) {
            
            foreach (var s in db.Schools) {
                var userRankings = GetRankingsList(s.Users, u => u.Points);
                
                foreach (var r in userRankings) {
                    r.Item.SchoolRank = r.Rank;
                }
            }

            foreach (var c in db.Countries) {
                var userRankings = GetRankingsList(c.Users, u => u.Points);
                var schoolRankings = GetRankingsList(c.Schools, s => s.Points);
                
                foreach (var r in userRankings) {
                    r.Item.CountryRank = r.Rank;
                }

                foreach (var r in schoolRankings) {
                    //TODO: update school country rank                    
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
            if (rateable.Rating.Count == 0) {
                return rateable.AvgPoints;
            }

            return rateable.AvgPoints * (rateable.Rating.Popularity / avgPopularity);
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
