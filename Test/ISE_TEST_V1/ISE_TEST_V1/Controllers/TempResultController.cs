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
        Result_1Entities1 ttr_db = new Result_1Entities1();
        Answer_Log al_db = new Answer_Log();
        QuestionsEntities1 q_db = new QuestionsEntities1();
        AnswersEntities a_db = new AnswersEntities();

        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Test(int id)
        {
            //var qt = from q in qt_db.Q_T_R where q.T_ID

            var al_q = from al in al_db.Answers_Log where al.C_T_R_ID.Equals(id) select al;
            List<Answers_Log> ans_log = new List<Answers_Log>();
            List<Question> q = new List<Question>();
            Dictionary<int, int> uniqueStore = new Dictionary<int, int>();
            List<Answer> us_a = new List<Answer>();
            List<A_List> u_a = new List<A_List>();
            List<A_List> a = new List<A_List>();


            ans_log = al_q.ToList();
            if (ans_log.Any())
            {

                for (int i = 0; i < ans_log.Count; i++)
                {
                    
                    
                    int q_id = ans_log.ElementAt(i).Q_ID;
                    if (!uniqueStore.ContainsKey(q_id))
                    {
                        uniqueStore.Add(q_id, 0);
                        List<Answers_Log> result = ans_log.FindAll(delegate(Answers_Log _myans)
                        {

                            return _myans.Q_ID == q_id;

                        });
                        for (int w = 0; w < result.Count; w++)
                        {
                            Answer user_answer = new Answer();
                            user_answer.ID = result.ElementAt(w).A_ID;
                            user_answer.PID = result.ElementAt(w).PID;
                            user_answer.is_Right = result.ElementAt(w).is_right;
                            user_answer.Q_ID = result.ElementAt(w).Q_ID;
                            user_answer.Text = result.ElementAt(w).Text;
                            user_answer.value = result.ElementAt(w).value;
                            if (us_a == null)
                            {
                                us_a = new List<Answer>();
                            }
                            us_a.Add(user_answer);
                        }
                        Question que = q_db.Questions.Single(qq => qq.ID.Equals(q_id));
                        q.Add(que);

                        var answers = from an in a_db.Answers where an.Q_ID.Equals(q_id) select an;
                        List<Answer> temp_a = new List<Answer>();
                        temp_a = answers.ToList();
                        A_List my_a = new A_List();
                        my_a.my_List = new List<Answer>();
                        my_a.my_List = temp_a;
                        a.Add(my_a);
                        A_List my_u_a = new A_List();
                        my_u_a.my_List = new List<Answer>();
                        my_u_a.my_List = us_a;
                        u_a.Add(my_u_a);
                        us_a = null;
                        temp_a = null;

                    }





                }


            }
            var ViewModel = new T_Detaile
            {
                Q = q,
                Answers = a,
                U_Answers = u_a

            };

            return View(ViewModel);

        }

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Page(int id)
        {
            int PAGE_SIZE = 20;
            var Candidates = from q in db.Candidates select q;
            List<Candidate> c1 = Candidates.ToList();
            PagedList<Candidate> data = new PagedList<Candidate>(c1, id, PAGE_SIZE);
            return View(data);
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
            List<Result> my_results = new List<Result>();
            List<bool> ind = new List<bool>();
            for (int i = 0; i < my_list.Count(); i++)
            {
            C_T_R my_C_T_R = my_list.ElementAt(i);
            Test t = t_db.Tests.Single(a => a.ID == my_C_T_R.T_ID);
            Temp_Result tr = tr_db.Temp_Result.Single(a => a.C_T_R_ID == my_C_T_R.ID);
            var ttr_q = from ttrl in ttr_db.Results
                        where (ttrl.C_T_R_ID == my_C_T_R.ID)
                        select ttrl;
            List<Result> my = ttr_q.ToList();
            my_tests.Add(t);
            my_temp_results.Add(tr);
            if (my.Any())
            {
                my_results.AddRange(my);
                ind.Add(true);
            }
            else
            {
                ind.Add(false);
            }
            }
            var viewModel = new Temp_Reesult_Details
            {
                c = candidate,
                t = my_tests,
                t_r = my_temp_results,
                tt_r = my_results,
                ind = ind
                
            };
            return View(viewModel); 


        }
        public ActionResult T_Details(int id)
        {

            return View();
        }

    }
}
