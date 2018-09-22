using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections;

namespace Fudge.Web.Points {
    class Program {

        //const int AvgProblem = 6;
        //const int AvgContestProblem = 20;
        //const int AvgForumPost = 1;
        //const int AvgEditorial = 1500;
        //const int AvgBlogPost = 5;

        static double GetAveragePopularity(IEnumerable rateables) {
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

        static double GetPoints(IEnumerable rateables, IEnumerable pool) {
            return GetPoints(rateables, GetAveragePopularity(pool));
        }

        static double GetPoints(IEnumerable rateables, double avgPopularity) {
            double points = 0;

            foreach (IRateable rateable in rateables) {
                points += GetPoints(rateable, avgPopularity);
                Console.WriteLine("\tRateable {0} yielded {1} pts", rateable.Rating.RatingId, GetPoints(rateable, avgPopularity));
            }

            return points;
        }

        static double GetPoints(IRateable rateable, double avgPopularity) {
            if (rateable.Rating.Count == 0) {
                return rateable.AvgPoints;
            }

            return rateable.AvgPoints * (rateable.Rating.Popularity / avgPopularity);
        }

        /// <summary>
        /// Makes a ranking list from a collection of items
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="initial">initial collection</param>
        /// <param name="selector">projection function to apply to each element</param>
        /// <returns></returns>
        public static IEnumerable<RankTuple<T>> MakeRankingList<T>(IEnumerable<T> initial, Func<T, int> selector) where T : class {
            var rankList = initial.OrderByDescending(selector).ToArray();
            List<RankTuple<T>> ranked = new List<RankTuple<T>>();
            int tieRank = 0, rank = 1;
            for (int i = 0; i < rankList.Length; ++i) {
                if (i > 0 && selector(rankList[i - 1]) == selector(rankList[i])) {
                    tieRank++;
                    ranked.Add(new RankTuple<T> { Item = rankList[i], Rank = ranked.Last().Rank });
                }
                else {
                    rank += tieRank;
                    ranked.Add(new RankTuple<T> { Item = rankList[i], Rank = rank++ });
                    tieRank = 0;
                }
            }
            return ranked;
        }

        public class RankTuple<T> {
            public int Rank { get; set; }
            public T Item { get; set; }
        }

        static void Main(string[] args) {
            FudgeDataContext db = new FudgeDataContext(@"Data Source=163.118.202.41,81;Initial Catalog=fudge;Persist Security Info=True;User ID=webuser;Password=webpassword");

            double runAvgPopularity = GetAveragePopularity(db.Runs);
            double postAvgPopularity = GetAveragePopularity(db.Posts);

            Console.WriteLine("Average run popularity is {0}", runAvgPopularity);
            Console.WriteLine("Average post popularity is {0}", postAvgPopularity);

            foreach (User user in db.Users) {
                double userPoints = 0.0;

                Console.WriteLine("User: {0}", user.FirstName);

                userPoints += GetPoints(user.GoodRuns, runAvgPopularity);
                userPoints += GetPoints(user.Posts.Where(p => p.Rating.Count > 0), postAvgPopularity);

                user.Points = (int)Math.Round(userPoints);
            }

            db.SubmitChanges();

            //global rank
            var globalUserRank = MakeRankingList(db.Users, u => u.Points);
            var globalSchoolRank = MakeRankingList(db.Schools, s => s.Points);
            var globalRegionRank = MakeRankingList(db.Regions, r =>
                r.Schools.Any() ?
                Convert.ToInt32(r.Schools.Average(s => s.Points))
                : 0);

            //update global rankings
            foreach (var r in globalUserRank) {
                r.Item.GlobalRank = r.Rank;
            }
            foreach (var r in globalSchoolRank) {
                r.Item.GlobalRank = r.Rank;
            }
            foreach (var r in globalRegionRank) {
                r.Item.GlobalRank = r.Rank;
            }

            //update user school ranking
            foreach (var s in db.Schools) {
                var userRankings = MakeRankingList(s.Users, u => u.Points);
                //iterate through all rankings for this school and update the user school rank
                foreach (var r in userRankings) {
                    r.Item.SchoolRank = r.Rank;
                }
            }
            //update user school ranking
            foreach (var c in db.Countries) {
                var userRankings = MakeRankingList(c.Users, u => u.Points);
                var schoolRankings = MakeRankingList(c.Schools, s => s.Points);
                //iterate through all rankings for this school and update the user country rank
                foreach (var r in userRankings) {
                    r.Item.CountryRank = r.Rank;
                }
                foreach (var r in schoolRankings) {
                    //update school country rank                    
                }
            }


            foreach (var region in db.Regions) {
                //get all users in this region
                var users = from s in region.Schools
                            from u in s.Users
                            select u;
                var userRankings = MakeRankingList(users, u => u.Points);
                foreach (var r in userRankings) {
                    r.Item.RegionRank = r.Rank;
                }
            }
            db.SubmitChanges();
        }
    }
}
