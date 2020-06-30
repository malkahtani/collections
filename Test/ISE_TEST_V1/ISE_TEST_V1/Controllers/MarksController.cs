using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using ISE_TEST_V1.ViewModels;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Web.Security;

namespace ISE_TEST_V1.Controllers
{
    public class MarksController : Controller
    {
        //
        // GET: /Marks/

        Answer_Log db = new Answer_Log();
        Candidate1_Entities c_db = new Candidate1_Entities();
        Tests_Entities t_db = new Tests_Entities();
        Can_TestsEntities c_t_db = new Can_TestsEntities();
        QuestionsEntities1 q_db = new QuestionsEntities1();
        AnswersEntities a_db = new AnswersEntities();
        Marks_dbEntities m_db = new Marks_dbEntities();
        TempResultEntities tr_db = new TempResultEntities();
        Result_1Entities1 r_db = new Result_1Entities1();
        TestM_Role_Entities test_m = new TestM_Role_Entities();
        QTEntities12 qt_db = new QTEntities12();
        
     
        

        public ActionResult Error(string massage)
        {


            ViewData["error"] = massage;

            return View();




        }
        public ActionResult Index()
        {
            bool there_is = false;
            int int_loop = 0;
            
            var A_L = from al in db.Answers_Log where (al.is_marked == false || al.need_rev == true) orderby al.C_T_R_ID select al;
            List<Answers_Log> al_list = A_L.ToList<Answers_Log>();
            List<int> C_T_R_IDs = new List<int>();
            List<C_T_R> my_ctr_list = new List<C_T_R>();
            List<Test> t_list = new List<Test>();
            List<Candidate> c_list = new List<Candidate>();
            
            //Auth
            List<int> tids = new List<int>();
            
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            List<TestM_Role> MyList = new List<TestM_Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                string rr = roles[i];
                var r_t = from t in test_m.TestM_Role where (t.RoleName == rr) select t;
                if (r_t.Any())
                {
                    List<TestM_Role> intlist = new List<TestM_Role>();
                    intlist = r_t.ToList();
                    MyList.AddRange(intlist);

                }

            }





            for (int x = 0; x < MyList.Count; x++)
            {
                int t_id = MyList.ElementAt(x).Test_ID;
                Test Tests = t_db.Tests.Single(a => a.ID.Equals(t_id));
                int test_id = 0;

                test_id = Tests.ID;
                tids.Add(test_id);

            }
            // End Auth
            

            List<List_AL> answers = new List<List_AL>();
            int loop = al_list.Count();
            for (int i = 0; i < loop; i++)
            {
                int ctr_id = al_list.ElementAt(i).C_T_R_ID;
                if (!C_T_R_IDs.Contains(ctr_id))
                {
                    C_T_R_IDs.Add(ctr_id);

                }

            }
            int nloop = C_T_R_IDs.Count();
            for (int j = 0; j < nloop; j++)
            {
                
                int ctr_id = C_T_R_IDs.ElementAt(j);
                C_T_R my_CTR = c_t_db.C_T_R.Single(a => a.ID == ctr_id);
                
                int c_id = my_CTR.C_ID;
                int t_id = my_CTR.T_ID;
                if (tids.Contains(t_id))
                {
                    my_ctr_list.Add(my_CTR);
                    Test my_t = t_db.Tests.Single(a => a.ID == t_id);
                    Candidate my_c = c_db.Candidates.Single(a => a.ID == c_id);
                    t_list.Add(my_t);
                    c_list.Add(my_c);
                    /*List_AL f_a = new List_AL();
                    f_a.need_rev = new List<Answers_Log>();
                    f_a.not_marked = new List<Answers_Log>();*/
                    List_AL f_a = new List_AL();
                    answers.Add(f_a);
                    answers.ElementAt(int_loop).need_rev = new List<Answers_Log>();
                    answers.ElementAt(int_loop).not_marked = new List<Answers_Log>();
                    var A_L_1 = from al_1 in db.Answers_Log where (al_1.is_marked == false && al_1.C_T_R_ID == ctr_id) select al_1;
                    answers.ElementAt(int_loop).not_marked = A_L_1.ToList();
                    var A_L_2 = from al_2 in db.Answers_Log where (al_2.need_rev == true && al_2.C_T_R_ID == ctr_id) select al_2;
                    answers.ElementAt(int_loop).need_rev = A_L_2.ToList();
                    there_is = true;
                    
                       
                    
                    
                    int_loop = int_loop + 1;
                }
               // answers.Add(f_a);

            }
            if (there_is)
            {
                var viewModel = new MarksIndexView
                {
                    count = int_loop,
                    ctr = my_ctr_list,
                    can = c_list,
                    test = t_list,
                    answers_log = answers
                };
                
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Error", "Marks", new { massage = "There is no Test to mark under your permissions" });
            }
          /*  for (int k = 0; k < my_ctr_list.Count(); k++)
            {
                

            }
            */


           
        }
        // Here
        public ActionResult Mark(int id)
        {
            //Auth
            List<int> tids = new List<int>();

            string user = System.Web.HttpContext.Current.User.Identity.Name;
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            List<TestM_Role> MyList = new List<TestM_Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                string rr = roles[i];
                var r_t = from t in test_m.TestM_Role where (t.RoleName == rr) select t;
                if (r_t.Any())
                {
                    List<TestM_Role> intlist = new List<TestM_Role>();
                    intlist = r_t.ToList();
                    MyList.AddRange(intlist);

                }

            }





            for (int x = 0; x < MyList.Count; x++)
            {
                int t_id = MyList.ElementAt(x).Test_ID;
                Test Tests = t_db.Tests.Single(a => a.ID.Equals(t_id));
                int test_id = 0;

                test_id = Tests.ID;
                tids.Add(test_id);

            }
            C_T_R my_CTR = c_t_db.C_T_R.Single(a => a.ID == id);
            int test_id_checker = my_CTR.T_ID;

            // End Auth
            
            if(tids.Contains(test_id_checker)){
            List<Answers_Log> my_al = new List<Answers_Log>();
            List<mark> my_mark = new List<mark>();
            List<MarksView> viewModel = new List<MarksView>();
            

            var A_L_1 = from al_1 in db.Answers_Log where ((al_1.is_marked == false || al_1.need_rev == true) && al_1.C_T_R_ID == id) select al_1;
            my_al = A_L_1.ToList();

            int loop = my_al.Count();
            for (int i = 0; i < loop; i++)
            {
                mark this_mark = new mark();
                int q_id = my_al.ElementAt(i).Q_ID;
                Question my_q = new Question();
                my_q = q_db.Questions.Single(a => a.ID == q_id);
                List<Answer> this_ans = new List<Answer>();
                var ans = from ans_1 in a_db.Answers where (ans_1.Q_ID == my_q.ID) select ans_1;
                this_ans = ans.ToList();
                Answers_Log my_ans_log = new Answers_Log();
                Mark my_db_mark = new Models.Mark();
                my_ans_log = my_al.ElementAt(i);
                this_mark.A = this_ans;
                this_mark.AL = my_ans_log;
                this_mark.Q = my_q;
                my_db_mark.AL_ID = my_ans_log.ID; my_db_mark.C_T_R_ID = my_ans_log.C_T_R_ID; my_db_mark.Q_ID = my_q.ID;
                this_mark.db_mark = my_db_mark;
                
                MarksView my_m_v = new MarksView();
                
                my_m_v.A = this_mark.A;
                my_m_v.AL = this_mark.AL;
                my_m_v.db_mark = this_mark.db_mark;
                my_m_v.Q = this_mark.Q;
               viewModel.Add(my_m_v);
                
            
            
            }
            return View(viewModel); 
            }else{
                return RedirectToAction("LogOn", "Account");
            }
        }

        [HttpPost, ValidateInput(false)] 
        public ActionResult Go_Marks(ICollection<mark> m)
        {
            List<mark> my_marks = m.Cast<mark>().ToList();
            /*for (int z = 0; z < my_marks.Count(); z++)
            {
                my_marks.ElementAt(z).AL.Text = System.Web.HttpUtility.HtmlDecode(my_marks.ElementAt(z).AL.Text);
                my_marks.ElementAt(z).Q.Text = System.Web.HttpUtility.HtmlDecode(my_marks.ElementAt(z).Q.Text);
                List<Answer> l = my_marks.ElementAt(z).A;
                for (int i = 0; i < l.Count(); i++)
                {
                    my_marks.ElementAt(z).A.ElementAt(i).Text = System.Web.HttpUtility.HtmlDecode(my_marks.ElementAt(z).A.ElementAt(i).Text);
                }

            }*/
            int my_loop = my_marks.Count();
            Answers_Log my_alog_0 = m.ElementAt(0).AL;
            int C_T_ID = my_alog_0.C_T_R_ID;
             //Auth
            List<int> tids = new List<int>();

            string user = System.Web.HttpContext.Current.User.Identity.Name;
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            List<TestM_Role> MyList = new List<TestM_Role>();
            for (int i = 0; i < roles.Count(); i++)
            {
                string rr = roles[i];
                var r_t = from t in test_m.TestM_Role where (t.RoleName == rr) select t;
                if (r_t.Any())
                {
                    List<TestM_Role> intlist = new List<TestM_Role>();
                    intlist = r_t.ToList();
                    MyList.AddRange(intlist);

                }

            }





            for (int x = 0; x < MyList.Count; x++)
            {
                int t_id = MyList.ElementAt(x).Test_ID;
                Test Tests = t_db.Tests.Single(a => a.ID.Equals(t_id));
                int test_id = 0;

                test_id = Tests.ID;
                tids.Add(test_id);

            }
            C_T_R my_CTR = c_t_db.C_T_R.Single(a => a.ID == C_T_ID);
            int test_id_checker = my_CTR.T_ID;

            // End Auth

            if (tids.Contains(test_id_checker))
            {
                double user_mark = 0;
                double total_mark = 0;
                int n_Q = 0;
                double new_marks = 0;

                for (int i = 0; i < my_loop; i++)
                {
                    Mark my_mark = m.ElementAt(i).db_mark;
                    
                    new_marks = new_marks + my_mark.User_Mark;
                    int my_id = m.ElementAt(i).AL.ID;
                    Answers_Log my_alog = db.Answers_Log.Single(a => a.ID == my_id);
                    my_alog.is_marked = true;
                    my_alog.need_rev = false;
                   
                    
                    m_db.AddToMarks(my_mark);
                    m_db.SaveChanges();
                    //UpdateModel(my_alog);
                    db.SaveChanges();
                    Temp_Result tr = tr_db.Temp_Result.Single(a => a.C_T_R_ID == my_alog.C_T_R_ID);
                    user_mark = tr.User_Marks;
                    total_mark = tr.Total_Marks;
                    n_Q = tr.N_Q_M;


                }
                Result my_r = new Result();
                my_r.C_T_R_ID = my_alog_0.C_T_R_ID;
                my_r.N_Q_M = n_Q + my_loop;
                my_r.Total_Marks = total_mark;
                my_r.User_Marks = user_mark + new_marks;
                my_r.Result1 = (my_r.User_Marks / my_r.Total_Marks) * 100;
                r_db.AddToResults(my_r);
                r_db.SaveChanges();
                Test marked_test = t_db.Tests.Single(a => a.ID.Equals(test_id_checker));
                string [] users = System.Web.Security.Roles.GetUsersInRole("HR");
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("no-replay@ise-ltd.com");
                    for (int i = 0; i < users.Count(); i++)
                    {
                        string email = Membership.GetUser(users[i]).Email;
                        message.To.Add(new MailAddress(email));
                    }
                    string marker_email = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).Email;
                    message.Bcc.Add(marker_email);
                    message.Subject = "A Test appointment has been marked";
                    message.Body = "";
                    message.IsBodyHtml = false;
                    string bodyHtml = "<p> The Test " + marked_test.Name + " Has been marked by " + user + " and the result can be checked on the link <br>"
                        + "http://ksaisewb01/Test/TempResult/C_Details/" + my_CTR.C_ID + "<br>";
                    using (AlternateView altView = AlternateView.CreateAlternateViewFromString(bodyHtml, new ContentType(MediaTypeNames.Text.Html)))
                    {
                        message.AlternateViews.Add(altView);
                        SmtpClient smtp = new SmtpClient("Ksaisefe01.ise.local");
                        smtp.Credentials = new NetworkCredential("ise\\isewb", "isewb01");
                        try
                        {
                            smtp.Send(message);

                        }
                        catch (Exception ex)
                        {
                            return RedirectToAction("Error", "Marks", new { massage = "Can not notify the HR that the Test has been marked" });
                        }
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Marks", new { massage = "you do not have permission to mark this test" });
            }
        }
        static List<int> removeDuplicates(List<int> inputList)
        {
            Dictionary<int, int> uniqueStore = new Dictionary<int, int>();
            List<int> finalList = new List<int>();
            foreach (int currValue in inputList)
            {
                if (!uniqueStore.ContainsKey(currValue))
                {
                    uniqueStore.Add(currValue, 0);
                    finalList.Add(currValue);
                }
            }
            return finalList;
        }

        

    }
}
