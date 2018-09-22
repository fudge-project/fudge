using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Fudge.Framework.Database {
    public static class ModelExtensions {

        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> list) where T : class {
            return list.ToPagedList(Html.DefaultPageSize);
        }

        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> list, int pageSize) where T : class {
            int pageIndex = 0;
            if (Int32.TryParse(HttpContext.Current.Request["page"], out pageIndex)) {
                pageIndex--;
            }
            return list.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> list, int pageIndex, int pageSize) where T : class {
            return list.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> list, int pageIndex, int pageSize) where T : class {
            return list.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ToRandom<T>(this IEnumerable<T> list) {
            Random random = new Random();       
            return (from item in list
                   orderby random.Next()
                   select item).AsQueryable();
        }

        public static IQueryable<Run> SortBy(this IQueryable<Run> runs, string sortExpression, SortDirection direction) {
            if (sortExpression == "Language") {
                switch (direction) {
                    case SortDirection.Ascending:
                        runs = runs.OrderBy(r => r.Language.Name);
                        break;
                    case SortDirection.Descending:
                        runs = runs.OrderByDescending(r => r.Language.Name);
                        break;
                    default:
                        break;
                }
            }
            else if (sortExpression == "Problem") {
                switch (direction) {
                    case SortDirection.Ascending:
                        runs = runs.OrderBy(r => r.Problem.Name);
                        break;
                    case SortDirection.Descending:
                        runs = runs.OrderByDescending(r => r.Problem.Name);
                        break;
                    default:
                        break;
                }
            }
            else if (sortExpression == "User") {
                switch (direction) {
                    case SortDirection.Ascending:
                        runs = runs.OrderBy(r => r.User.FirstName + " " + r.User.LastName);
                        break;
                    case SortDirection.Descending:
                        runs = runs.OrderByDescending(r => r.User.FirstName + " " + r.User.LastName);
                        break;
                    default:
                        break;
                }
            }
            else if (sortExpression == "RunId") {
                switch (direction) {
                    case SortDirection.Ascending:
                        runs = runs.OrderBy(r => r.RunId);
                        break;
                    case SortDirection.Descending:
                        runs = runs.OrderByDescending(r => r.RunId);
                        break;
                    default:
                        break;
                }
            }
            return runs;
        }               
    }
}