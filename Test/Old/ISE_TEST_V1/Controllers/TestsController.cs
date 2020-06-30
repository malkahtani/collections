using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;



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

        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Index()
        {
            var Tests = from t in db.Tests select t;
            return View(Tests.ToList());
        }

        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Create() { return View(); }
        [HttpPost, Authorize(Roles = "Admin,Power_User")]
        public ActionResult Create(Test newTest)
        {
            if (ModelState.IsValid)
            {
                db.AddToTests(newTest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(newTest);
            }
        }
        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Details(int id)
        {
            Test test = db.Tests.Single(a => a.ID == id); return View(test);
        }
        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Edit(int id)
        {
            Test test = db.Tests.Single(a => a.ID == id); return View(test);
        }


        [HttpPost, Authorize(Roles = "Admin,Power_User")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var test = db.Tests.Single(a => a.ID == id);
            try
            {

                UpdateModel(test);
                db.SaveChanges();
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
        }
        [Authorize(Roles = "Admin,Power_User")]
        public ActionResult Delete(int id)
        {
            Test test = db.Tests.Single(a => a.ID == id); return View(test);
        }
        [HttpPost, Authorize(Roles = "Admin,Power_User")]
        public ActionResult Delete(int id, string confirmButton)
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
    }
}
