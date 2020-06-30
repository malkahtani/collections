using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using ISE_TEST_V1.ViewModels;
namespace ISE_TEST_V1.Controllers
{
    public class TempResultController : Controller
    {
        //
        // GET: /TempResult/
        Candidate1_Entities db = new Candidate1_Entities();
        Tests_Entities t_db = new Tests_Entities();
        TempResultEntities tr_db = new TempResultEntities();
        Can_TestsEntities c_t_r_db = new Can_TestsEntities();
        public ActionResult Index()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Search(string searchTerm, int by)
        {
            if (searchTerm == string.Empty)
            {
                return View();
            }
            else
            {
                // if the search contains only one result return details
                // otherwise a list
                
                if (by == 0)
                {
                    int id = Convert.ToInt32(searchTerm);
                     
                   var  c = from a in db.Candidates
                            where a.ID.Equals(id)
                            orderby a.ID
                            select a;
                   if (c.Count() == 0)
                   {
                       return View("notfound");
                   }

                   if (c.Count() > 1)
                   {
                       return View("C_List", c);
                   }
                   else
                   {
                       return RedirectToAction("C_Details",
                           new { id = c.First().ID });
                   }
                }
                else if (by == 1)
                {
                   var  c = from a in db.Candidates
                            where a.Uname.Contains(searchTerm)
                            orderby a.Name
                            select a;
                   if (c.Count() == 0)
                   {
                       return View("notfound");
                   }

                   if (c.Count() > 1)
                   {
                       return View("C_List", c);
                   }
                   else
                   {
                       return RedirectToAction("C_Details",
                           new { id = c.First().ID });
                   }
                }
                else if (by == 2)
                {
                    var c = from a in db.Candidates
                            where a.Name.Contains(searchTerm)
                            orderby a.Name
                            select a;
                    if (c.Count() == 0)
                    {
                        return View("notfound");
                    }

                    if (c.Count() > 1)
                    {
                        return View("C_List", c);
                    }
                    else
                    {
                        return RedirectToAction("C_Details",
                            new { id = c.First().ID });
                    }
                }
                else if (by == 3)
                {
                    int mobile = Convert.ToInt32(searchTerm);
                   var  c = from a in db.Candidates
                           
                            where a.Mobile.Equals(mobile)
                            orderby a.Name
                            select a;
                     if (c.Count() == 0)
                     {
                         return View("notfound");
                     }

                     if (c.Count() > 1)
                     {
                         return View("C_List", c);
                     }
                     else
                     {
                         return RedirectToAction("C_Details",
                             new { id = c.First().ID });
                     }
                }
                else if (by == 4)
                {
                    var c = from a in db.Candidates
                            where a.Email.Contains(searchTerm)
                            orderby a.Name
                            select a;
                    if (c.Count() == 0)
                    {
                        return View("notfound");
                    }

                    if (c.Count() > 1)
                    {
                        return View("C_List", c);
                    }
                    else
                    {
                        return RedirectToAction("C_Details",
                            new { id = c.First().ID });
                    }
                }
                else
                {
                    return View("Error");
                }
                
               
            }
        }
        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult C_Details(int id)
        {
            Candidate candidate = db.Candidates.Single(a => a.ID == id);
            var c_t_r_q = from a in c_t_r_db.C_T_R
                    where a.C_ID.Equals(id) && a.is_submitted.Equals(true)
                    orderby a.Date_to_Set
                    select a;
            List<C_T_R> my_list = c_t_r_q.ToList<C_T_R>();
            List<Test> my_tests = new List<Test>();
            List<Temp_Result> my_temp_results = new List<Temp_Result>();
            for (int i = 0; i < my_list.Count(); i++)
            {
            C_T_R my_C_T_R = my_list.ElementAt(i);
            Test t = t_db.Tests.Single(a => a.ID == my_C_T_R.T_ID);
            Temp_Result tr = tr_db.Temp_Result.Single(a => a.C_T_R_ID == my_C_T_R.ID);
            my_tests.Add(t);
            my_temp_results.Add(tr);

            }
            var viewModel = new Temp_Reesult_Details
            {
                c = candidate,
                t = my_tests,
                t_r = my_temp_results
                
            };
            return View(viewModel); 


        }
    }
}
