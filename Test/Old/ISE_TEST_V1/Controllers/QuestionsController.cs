using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using ISE_TEST_V1.ViewModels;
using System.Data;

namespace ISE_TEST_V1.Controllers
{
    public class QuestionsController : Controller
    {
        //
        // GET: /Questions/
        Tests_Entities t_db = new Tests_Entities();
        QuestionsEntities1 q_db = new QuestionsEntities1();
        QTEntities12 qt_db = new QTEntities12();
        AnswersEntities a_db = new AnswersEntities();
         [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Index()
        {
           var Questions = from q in q_db.Questions select q;
           return View(Questions.ToList()); 
           
           
        }
         [Authorize(Roles = "Admin,Power_User,HR")]
         public ActionResult Link_To_Test(int id)
         {
             var c = q_db.Questions.Single(a => a.ID == id);
             var Tests = from t in t_db.Tests select t;
             List<Test> MyList = Tests.ToList<Test>();
             List<int> tids = new List<int>();
             List<String> tnames = new List<String>();
             for (int i = 0; i < MyList.Count; i++)
             {
                 int test_id = 0;
                 String test_name = "";
                 test_id = MyList[i].ID;
                 test_name = MyList[i].Name;
                 tids.Add(test_id);
                 tnames.Add(test_name);
             }
             var viewModel = new Q_T_IndexViewModel
             {
                 QID = id,
                 QType = c.Type,
                 TID = tids,
                 TName = tnames
             };
             return View(viewModel);

         }
         [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Q_T(int qid, int tests, int qtype)
        {
            Q_T_R q = new Q_T_R();
            q.Q_ID = qid;
            q.T_ID = tests;
            q.Q_Type = (short) qtype;
            qt_db.AddToQ_T_R(q);
            qt_db.SaveChanges();
            return View("Linked");
         }
         [Authorize(Roles = "Admin,Power_User")]
         public ActionResult Test()
         {
             var Tests = from t in t_db.Tests select t;
             List<Test> MyList = Tests.ToList<Test>();
             List<int> tids = new List<int>();
             List<String> tnames = new List<String>();
             for (int i = 0; i < MyList.Count; i++)
             {
                 int test_id = 0;
                 String test_name = "";
                 test_id = MyList[i].ID;
                 test_name = MyList[i].Name;
                 tids.Add(test_id);
                 tnames.Add(test_name);

             }
             var viewModel = new QuestionsIndex
             {

                 TID = tids,
                 TName = tnames
             };
             return View(viewModel);


         }
         [Authorize(Roles = "Admin,Power_User")]
         public ActionResult Delete(int id)
         {
             Question q = q_db.Questions.Single(a => a.ID == id); return View(q);
         }
         [HttpPost, Authorize(Roles = "Admin,Power_User")]
         public ActionResult Delete(int id, string confirmButton)
         {
             Question q = q_db.Questions.Single(a => a.ID == id);
                    q_db.DeleteObject(q);
                     q_db.SaveChanges();

                
             return View("Deleted");

         }
         [Authorize(Roles = "Admin,Power_User")]
         public ActionResult Edit(int id)
         {
             Question q = q_db.Questions.Single(a => a.ID == id);
             var q_answers = from q_a in a_db.Answers where q_a.Q_ID.Equals(id) select q_a;
             List<Answer> q_a_list = q_answers.ToList<Answer>();

             return View(q);
         }


         [HttpPost, Authorize(Roles = "Admin,Power_User")]
         public ActionResult Edit(int id, FormCollection collection)
         {
             
                 return View();
             }
         
        [AcceptVerbs(HttpVerbs.Post),Authorize(Roles = "Admin,Power_User")]
        public ActionResult Q(int tests, int QType, int NoA)
        {
            if (QType == 1)
            {
               List<TF_VM> viewModel = new List<TF_VM>();
               
                for(int i=0; i<NoA; i++){
                    TF my_tf = new TF();
                   QuestionsClass my_q = new QuestionsClass();
                   TFAnswer my_tf_a = new ISE_TEST_V1.Models.TFAnswer();
                    my_tf.my_tf_answers = my_tf_a;
                    my_tf.q = my_q;
                 
                    TF_VM  my_tf_v = new TF_VM();
                    my_tf_v.TID = tests;
                    my_tf_v.NOA = NoA;
                    my_tf_v.tf = my_tf;
                    my_tf_v.qt = QType;
                    viewModel.Add(my_tf_v);

                }

                return View("TF",viewModel); 
            }
            else if (QType == 2)
            {
                List<MCSA_VM> viewModel = new List<MCSA_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    MCSA my_mcsa = new MCSA();
                    QuestionsClass my_q = new QuestionsClass();
                    MCSAAnswer my_mcsa_a = new ISE_TEST_V1.Models.MCSAAnswer();
                    my_mcsa.my_mcsa_answers = my_mcsa_a;
                    my_mcsa.q = my_q;

                    MCSA_VM my_mcsa_v = new MCSA_VM();
                    my_mcsa_v.TID = tests;
                    my_mcsa_v.NOA = NoA;
                    my_mcsa_v.mcsa = my_mcsa;
                    my_mcsa_v.qt = QType;
                    viewModel.Add(my_mcsa_v);

                }

                return View("MCSA", viewModel);
            }
            else if (QType == 3)
            {
                List<MCMA_VM> viewModel = new List<MCMA_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    MCMA my_mcma = new MCMA();
                    QuestionsClass my_q = new QuestionsClass();
                    MCMAAnswer my_mcma_a = new ISE_TEST_V1.Models.MCMAAnswer();
                    my_mcma.my_mcma_answers = my_mcma_a;
                    my_mcma.q = my_q;

                    MCMA_VM my_mcma_v = new MCMA_VM();
                    my_mcma_v.TID = tests;
                    my_mcma_v.NOA = NoA;
                    my_mcma_v.mcma = my_mcma;
                    my_mcma_v.qt = QType;
                    viewModel.Add(my_mcma_v);

                }

                return View("MCMA", viewModel);
            }
            else if (QType == 4)
            {
                List<FGS_VM> viewModel = new List<FGS_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    FGS my_fgs = new FGS();
                    QuestionsClass my_q = new QuestionsClass();
                    FGSAnswer my_fgs_a = new ISE_TEST_V1.Models.FGSAnswer();
                    my_fgs.my_fgs_answers = my_fgs_a;
                    my_fgs.q = my_q;

                    FGS_VM my_fgs_v = new FGS_VM();
                    my_fgs_v.TID = tests;
                    my_fgs_v.NOA = NoA;
                    my_fgs_v.fgs = my_fgs;
                    my_fgs_v.qt = QType;
                    viewModel.Add(my_fgs_v);

                }

                return View("FGS", viewModel);
            }
            else if (QType == 5)
            {
                List<FGI_VM> viewModel = new List<FGI_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    FGI my_fgi = new FGI();
                    QuestionsClass my_q = new QuestionsClass();
                    FGIAnswer my_fgi_a = new ISE_TEST_V1.Models.FGIAnswer();
                    my_fgi.my_fgi_answers = my_fgi_a;
                    my_fgi.q = my_q;

                    FGI_VM my_fgi_v = new FGI_VM();
                    my_fgi_v.TID = tests;
                    my_fgi_v.NOA = NoA;
                    my_fgi_v.fgi = my_fgi;
                    my_fgi_v.qt = QType;
                    viewModel.Add(my_fgi_v);

                }

                return View("FGI", viewModel);
            }
            else if (QType == 7)
            {
                List<SA_VM> viewModel = new List<SA_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    SA my_sa = new SA();
                    QuestionsClass my_q = new QuestionsClass();
                    SAAnswer my_sa_a = new ISE_TEST_V1.Models.SAAnswer();
                    my_sa.my_sa_answers = my_sa_a;
                    my_sa.q = my_q;

                    SA_VM my_sa_v = new SA_VM();
                    my_sa_v.TID = tests;
                    my_sa_v.NOA = NoA;
                    my_sa_v.sa = my_sa;
                    my_sa_v.qt = QType;
                    viewModel.Add(my_sa_v);

                }

                return View("SA", viewModel);
            }
            else if (QType == 8)
            {
                List<EA_VM> viewModel = new List<EA_VM>();

                for (int i = 0; i < NoA; i++)
                {
                    EA my_ea = new EA();
                    QuestionsClass my_q = new QuestionsClass();
                    EAAnswer my_ea_a = new ISE_TEST_V1.Models.EAAnswer();
                    my_ea.my_ea_answers = my_ea_a;
                    my_ea.q = my_q;

                    EA_VM my_ea_v = new EA_VM();
                    my_ea_v.TID = tests;
                    my_ea_v.NOA = NoA;
                    my_ea_v.ea = my_ea;
                    my_ea_v.qt = QType;
                    viewModel.Add(my_ea_v);

                }

                return View("EA", viewModel);
            }
            else if (QType == 6)
            {
                List<MT_VM> viewModel = new List<MT_VM>();

                for (int i = 0; i < NoA * 2; i++)
                {
                    MT my_mt = new MT();
                    QuestionsClass my_q = new QuestionsClass();
                    MTAnswer my_mt_a = new ISE_TEST_V1.Models.MTAnswer();
                    my_mt.my_mt_answers = my_mt_a;
                    my_mt.q = my_q;

                    MT_VM my_mt_v = new MT_VM();
                    my_mt_v.TID = tests;
                    my_mt_v.NOA = NoA;
                    my_mt_v.mt = my_mt;
                    my_mt_v.qt = QType;
                    viewModel.Add(my_mt_v);

                }

                return View("MT", viewModel);
            }
            else
            {
                return View();
            }
        }
        
        //Action method on HomeController
        [AcceptVerbs(HttpVerbs.Post),Authorize(Roles = "Admin,Power_User")]
        public ActionResult TF_Add(ICollection<TF_VM> my_tf)
        {
            List<TF_VM> viewModel = my_tf.Cast<TF_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short) viewModel.ElementAt(0).tf.q.Wight;
            Q.Text = viewModel.ElementAt(0).tf.q.Text;
            Q.Type = (short) viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
           // Db.AddObject("Employee", Emp) 
              q_db.AddToQuestions(Q);
              q_db.SaveChanges();
            
             EntityKey key = q_db.CreateEntityKey("Questions", Q);
             Q_T_R qt_r = new Q_T_R();
             
             int qkey =(int) key.EntityKeyValues[0].Value;
             qt_r.Q_ID = qkey;
             qt_r.T_ID = viewModel.ElementAt(0).TID;
             qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
             qt_db.AddToQ_T_R(qt_r);
             qt_db.SaveChanges();
             
             for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
             {
                 Answer my_answer = new Answer();
                 int order = j + 1;
                 my_answer.A_Order = (short) order;
                 my_answer.is_Right = viewModel.ElementAt(j).tf.my_tf_answers.is_right;
                 my_answer.PID = 0;
                 my_answer.Q_ID = qkey;
                 my_answer.Text = viewModel.ElementAt(j).tf.my_tf_answers.Text;
                 my_answer.value = 0;
                 a_db.AddToAnswers(my_answer);
                 a_db.SaveChanges();
             }
             ViewData["Message"] = "Your question has been added";

             return View();
            
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult MCSA_Add(ICollection<MCSA_VM> my_tf)
        {
            List<MCSA_VM> viewModel = my_tf.Cast<MCSA_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).mcsa.q.Wight;
            Q.Text = viewModel.ElementAt(0).mcsa.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                int order = j + 1;
                my_answer.A_Order = (short)order;
                my_answer.is_Right = viewModel.ElementAt(j).mcsa.my_mcsa_answers.is_right;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).mcsa.my_mcsa_answers.Text;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult MCMA_Add(ICollection<MCMA_VM> my_tf)
        {
            List<MCMA_VM> viewModel = my_tf.Cast<MCMA_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).mcma.q.Wight;
            Q.Text = viewModel.ElementAt(0).mcma.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                int order = j + 1;
                my_answer.A_Order = (short)order;
                my_answer.is_Right = viewModel.ElementAt(j).mcma.my_mcma_answers.is_right;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).mcma.my_mcma_answers.Text;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult FGS_Add(ICollection<FGS_VM> my_tf)
        {
            List<FGS_VM> viewModel = my_tf.Cast<FGS_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).fgs.q.Wight;
            Q.Text = viewModel.ElementAt(0).fgs.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                //int order = j + 1;
                my_answer.A_Order = 0;
                my_answer.is_Right = true;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).fgs.my_fgs_answers.Text;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult FGI_Add(ICollection<FGI_VM> my_tf)
        {
            List<FGI_VM> viewModel = my_tf.Cast<FGI_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).fgi.q.Wight;
            Q.Text = viewModel.ElementAt(0).fgi.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                //int order = j + 1;
                my_answer.A_Order = 0;
                my_answer.is_Right = true;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = "";
                my_answer.value = viewModel.ElementAt(j).fgi.my_fgi_answers.Value;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult SA_Add(ICollection<SA_VM> my_tf)
        {
            List<SA_VM> viewModel = my_tf.Cast<SA_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).sa.q.Wight;
            Q.Text = viewModel.ElementAt(0).sa.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                //int order = j + 1;
                my_answer.A_Order = 0;
                my_answer.is_Right = true;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).sa.my_sa_answers.Text;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult EA_Add(ICollection<EA_VM> my_tf)
        {
            List<EA_VM> viewModel = my_tf.Cast<EA_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).ea.q.Wight;
            Q.Text = viewModel.ElementAt(0).ea.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();

            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                //int order = j + 1;
                my_answer.A_Order = 0;
                my_answer.is_Right = true;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).ea.my_ea_answers.Text;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User")]
        public ActionResult MT_Add(ICollection<MT_VM> my_tf)
        {
            List<MT_VM> viewModel = my_tf.Cast<MT_VM>().ToList();
            Question Q = new Question();
            Q.Wight = (short)viewModel.ElementAt(0).mt.q.Wight;
            Q.Text = viewModel.ElementAt(0).mt.q.Text;
            Q.Type = (short)viewModel.ElementAt(0).qt;
            Q.N_Answers = (short)viewModel.ElementAt(0).NOA;
            // Db.AddObject("Employee", Emp) 
            q_db.AddToQuestions(Q);
            q_db.SaveChanges();

            EntityKey key = q_db.CreateEntityKey("Questions", Q);
            Q_T_R qt_r = new Q_T_R();

            int qkey = (int)key.EntityKeyValues[0].Value;
            qt_r.Q_ID = qkey;
            qt_r.T_ID = viewModel.ElementAt(0).TID;
            qt_r.Q_Type = (short)viewModel.ElementAt(0).qt;
            qt_db.AddToQ_T_R(qt_r);
            qt_db.SaveChanges();
            List<int> my_a_ids = new List<int>();
            for (int j = 0; j < viewModel.ElementAt(0).NOA; j++)
            {
                Answer my_answer = new Answer();
                //int order = j + 1;
                my_answer.A_Order = (short) viewModel.ElementAt(j).mt.my_mt_answers.A_Order;
                my_answer.is_Right = true;
                my_answer.PID = 0;
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(j).mt.my_mt_answers.TEXT;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
                EntityKey a_key = a_db.CreateEntityKey("Answers", my_answer);
                Q_T_R qt_r_1 = new Q_T_R();

                int akey = (int)a_key.EntityKeyValues[0].Value;
                my_a_ids.Add(akey);
            }
            int negloop = (viewModel.ElementAt(0).NOA * 2) - 1;
            for (int m = negloop; m >= viewModel.ElementAt(0).NOA; m--)
            {
                int index = viewModel.ElementAt(m).mt.my_mt_answers.PID;
                if (index != 0)
                {
                    index = index - 1;
                }
                
                Answer my_answer = new Answer();
                my_answer.A_Order = (short)viewModel.ElementAt(m).mt.my_mt_answers.A_Order;
                my_answer.is_Right = true;
                my_answer.PID = my_a_ids.ElementAt(index);
                my_answer.Q_ID = qkey;
                my_answer.Text = viewModel.ElementAt(m).mt.my_mt_answers.TEXT;
                my_answer.value = 0;
                a_db.AddToAnswers(my_answer);
                a_db.SaveChanges();
            }
            ViewData["Message"] = "Your question has been added";

            return View();
        }
    }
}
