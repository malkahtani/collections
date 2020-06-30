using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using ISE_TEST_V1.ViewModels;

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

        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Index()
        {
            var A_L = from al in db.Answers_Log where (al.is_marked == false || al.need_rev == true) orderby al.C_T_R_ID select al;
            List<Answers_Log> al_list = A_L.ToList<Answers_Log>();
            List<int> C_T_R_IDs = new List<int>();
            List<C_T_R> my_ctr_list = new List<C_T_R>();
            List<Test> t_list = new List<Test>();
            List<Candidate> c_list = new List<Candidate>();
            
            
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
                my_ctr_list.Add(my_CTR);
                int c_id = my_CTR.C_ID;
                int t_id = my_CTR.T_ID;
                Test my_t = t_db.Tests.Single(a => a.ID == t_id);
                Candidate my_c = c_db.Candidates.Single(a => a.ID == c_id);
                t_list.Add(my_t);
                c_list.Add(my_c);
                /*List_AL f_a = new List_AL();
                f_a.need_rev = new List<Answers_Log>();
                f_a.not_marked = new List<Answers_Log>();*/
                List_AL f_a = new List_AL();
                answers.Add(f_a);
                answers.ElementAt(j).need_rev = new List<Answers_Log>();
                answers.ElementAt(j).not_marked = new List<Answers_Log>();
                var A_L_1 = from al_1 in db.Answers_Log where (al_1.is_marked == false && al_1.C_T_R_ID == ctr_id) select al_1;
                answers.ElementAt(j).not_marked = A_L_1.ToList();
                var A_L_2 = from al_2 in db.Answers_Log where (al_2.need_rev == true && al_2.C_T_R_ID == ctr_id) select al_2;
                answers.ElementAt(j).need_rev = A_L_2.ToList();
               // answers.Add(f_a);

            }
            var viewModel = new MarksIndexView
            {
                ctr = my_ctr_list,
                can = c_list,
                test = t_list,
                answers_log = answers
            };
            return View(viewModel); 
          /*  for (int k = 0; k < my_ctr_list.Count(); k++)
            {
                

            }
            */


           
        }
        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Mark(int id)
        {
            
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
        }
        public ActionResult Go_Marks(ICollection<mark> m)
        {
            List<mark> my_marks = m.Cast<mark>().ToList();
            int my_loop = my_marks.Count();
            Answers_Log my_alog_0 = m.ElementAt(0).AL;
            double user_mark = 0 ;
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
            return View();
        }
    }
}
