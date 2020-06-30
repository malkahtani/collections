using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using System.Web.Security;
using System.Data;


namespace ISE_TEST_V1.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/
        Tests_Entities db = new Tests_Entities();
        QTEntities12 qt_db = new QTEntities12();
        QuestionsEntities1 q_db = new QuestionsEntities1();
        AnswersEntities a_db = new AnswersEntities();
        TestE_Role_Entities test_e = new TestE_Role_Entities();
        TestM_Role_Entities test_m = new TestM_Role_Entities();


        public ActionResult Index()
        {
            /* string user = System.Web.HttpContext.Current.User.Identity.Name;
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            List<TestE_Role> MyList = null;
            List<Test> TList = null;
            for (int i = 0; i < roles.Count(); i++)
            {
                 var r_t = from t in test_e.TestE_Role where t.RoleName== roles.ElementAt(i) select t;
                 MyList = r_t.ToList();
            }
            for (int j = 0; j < MyList.Count(); j++)
            {
                Test test = db.Tests.Single(a => a.ID == MyList.ElementAt(j).Test_ID);
                TList.Add(test);
            }*/
            ViewData["NoTests"] = "You Don't have permissions to Edit or create a test";

            return View();
        }
        public ActionResult Validate(int id)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {

               
            
            Test t = db.Tests.Single(a => a.ID == id);
            var tf = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 1)) select q;
            var mcsa = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 2)) select q;
            var mcma = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 3)) select q;
            var fgs = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 4)) select q;
            var fgi = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 5)) select q;
            var mt = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 6)) select q;
            var sa = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 7)) select q;
            var es = from q in qt_db.Q_T_R where ((q.T_ID == id) && (q.Q_Type == 8)) select q;
            if ((tf.Count() >= t.N_Q_TF) && (mcsa.Count() >= t.N_Q_MCSA) && (mcma.Count() >= t.N_Q_MCMA) && (fgs.Count() >= t.N_Q_FGS) && (fgi.Count() >= t.N_Q_FGI) && (mt.Count() >= t.N_Q_MT) && (sa.Count() >= t.N_Q_SA) && (es.Count() >= t.N_Q_EA))
            {
                t.valid = true;
                try
                {

                    UpdateModel(t);
                    db.SaveChanges();
                    // TODO: Add update logic here
                    return RedirectToAction("Page", new { id = 1 });
                }
                catch
                {

                    return View();
                }
            }
            return View();
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }
        public ActionResult Page(int id)
        {
            int PAGE_SIZE = 20;
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            List<TestE_Role> MyList = new List<TestE_Role>();
            List<Test> TList = new List<Test>();
            for (int i = 0; i < roles.Count(); i++)
            {
                string rr = roles[i];
                var r_t = from t in test_e.TestE_Role where (t.RoleName == rr) select t;
                if (r_t.Count() > 0)
                {
                    List<TestE_Role> intlist = new List<TestE_Role>();
                    intlist = r_t.ToList();
                    MyList.AddRange(intlist);

                }
            }
            if (MyList.Any())
            {
                for (int j = 0; j < MyList.Count(); j++)
                {
                    int test_id = MyList.ElementAt(j).Test_ID;
                    Test test = db.Tests.Single(a => a.ID == test_id);

                    TList.Add(test);
                }

                List<Test> t1 = TList;
                PagedList<Test> data = new PagedList<Test>(t1, id, PAGE_SIZE);
                return View(data);
            }
            else
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return RedirectToAction("Index", "Tests");
                }
            }
        }


        public ActionResult Create() {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }

        [HttpPost]
        public ActionResult Create(Test newTest)
        {
            if (ModelState.IsValid)
            {
                newTest.valid = false;
                db.AddToTests(newTest);
                db.SaveChanges();
                EntityKey key = db.CreateEntityKey("Tests", newTest);


                int qkey = (int)key.EntityKeyValues[0].Value;
                TestE_Role te = new TestE_Role();
                te.RoleName = newTest.role;
                te.Test_ID = qkey;
                test_e.AddToTestE_Role(te);
                test_e.SaveChanges();
                TestM_Role tm = new TestM_Role();
                tm.RoleName = newTest.m_role;
                tm.Test_ID = qkey;
                test_m.AddToTestM_Role(tm);
                test_m.SaveChanges();
                return RedirectToAction("Page", new { id = 1 });

            }
            else
            {
                return View(newTest);
            }
        }

        public ActionResult Details(int id)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {

                Test test = db.Tests.Single(a => a.ID == id); return View(test);
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }

        public ActionResult Edit(int id)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            TestM_Role m_t = test_m.TestM_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {

                Test test = db.Tests.Single(a => a.ID == id);
                test.role = role;
                test.m_role = m_t.RoleName;
                return View(test);
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {
                var test = db.Tests.Single(a => a.ID == id);
                try
                {

                    UpdateModel(test);
                    db.SaveChanges();
                    TestM_Role m = test_m.TestM_Role.Single(a => a.Test_ID == id);
                    m.RoleName = collection["m_role"];
                    test_m.SaveChanges();
                    TestE_Role e = test_e.TestE_Role.Single(a => a.Test_ID == id);
                    e.RoleName = collection["role"];
                    test_e.SaveChanges();

                    // TODO: Add update logic here
                    return RedirectToAction("Page", new { id = 1 });
                }
                catch
                {

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }

        public ActionResult Delete(int id)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {
                Test test = db.Tests.Single(a => a.ID == id); return View(test);
            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            TestE_Role r_t = test_e.TestE_Role.Single(t => t.Test_ID == id);
            string[] roles = System.Web.Security.Roles.GetRolesForUser(user);
            string role = r_t.RoleName;

            if (Array.Exists(roles, element => element == role))
            {
                var t = db.Tests.Single(a => a.ID == id);
                var q_t_result = from q_t in qt_db.Q_T_R where q_t.T_ID.Equals(id) select q_t;

                List<Q_T_R> result_list = q_t_result.ToList<Q_T_R>();

                for (int i = 0; i < result_list.Count(); i++)
                {
                    int ID = result_list.ElementAt(i).Q_ID;

                    var q_t_result_1 = from q_t_1 in qt_db.Q_T_R where q_t_1.Q_ID.Equals(ID) select q_t_1;
                    List<Q_T_R> q_t_result_1_List = q_t_result_1.ToList<Q_T_R>();
                    if (q_t_result_1_List.Count() == 1)
                    {
                        int Q_ID = result_list.ElementAt(i).Q_ID;
                        int Test_ID = result_list.ElementAt(i).T_ID;
                        var QT = qt_db.Q_T_R.Single(a => a.Q_ID == Q_ID && a.T_ID == Test_ID);
                        var Q = q_db.Questions.Single(a => a.ID == QT.Q_ID);
                        // qt_db.DeleteObject(QT);
                        // qt_db.SaveChanges();
                        /* var answers = from a in a_db.Answers where a.Q_ID.Equals(Q.ID) select a;
                         List<Answer> a_list = answers.ToList<Answer>();
                         for (int j = 0; j < answers.Count(); j++)
                         {
                             int a_id = a_list.ElementAt(j).ID;
                             Answer my_ans = a_db.Answers.Single(a => a.ID == a_id);
                             a_db.DeleteObject(my_ans);
                             a_db.SaveChanges();
                         }*/
                        q_db.DeleteObject(Q);
                        q_db.SaveChanges();

                    }
                    else
                    {
                        /* int Q_ID = result_list.ElementAt(i).Q_ID;
                         int Test_ID = result_list.ElementAt(i).T_ID;
                         var QT = qt_db.Q_T_R.Single(a => a.Q_ID == Q_ID && a.T_ID == Test_ID);
                         qt_db.DeleteObject(QT);
                         qt_db.SaveChanges();*/
                    }




                }
                db.DeleteObject(t);
                db.SaveChanges();
                return View("Deleted");

            }
            else
            {
                return RedirectToAction("Page", new { id = 1 });

            }
        }
    }
}
