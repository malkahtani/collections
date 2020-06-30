using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

using ISE_TEST_V1.Models;
using ISE_TEST_V1.ViewModels;
using System.Net.Mime;
using System.Web.Security;





namespace ISE_TEST_V1.Controllers
{
    public class CandidateController : Controller
    {

        
      Candidate1_Entities db = new Candidate1_Entities();
      Tests_Entities t_db = new Tests_Entities();
       Can_TestsEntities c_t_db = new Can_TestsEntities();
        [Authorize(Roles = "Admin,Power_User,HR")]

        public ActionResult Index()
        {
            var c = from t in db.Candidates select t;
            
            return View(c.ToList());

       

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
        public ActionResult Create() { return View(); }
        [HttpPost]
        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Create(Candidate newCandidate)
        {
            if (ModelState.IsValid)
            {
                db.AddToCandidates(newCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(newCandidate);
            }
        }
         [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Details(int id)
        {
            Candidate c = db.Candidates.Single(a => a.ID == id); return View(c);
        }
         [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult C_Details(int id)
        {
            Candidate c = db.Candidates.Single(a => a.ID == id); return View(c);
        }
         [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Edit(int id)
        {
            Candidate c = db.Candidates.Single(a => a.ID == id); return View(c);
        }


        [HttpPost, Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var c = db.Candidates.Single(a => a.ID == id);
            try
            {

                UpdateModel(c);
                db.SaveChanges();
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
        }
         [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Delete(int id)
        {
            Candidate c = db.Candidates.Single(a => a.ID == id); return View(c);
        }
        [HttpPost, Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Delete(int id, string confirmButton)
        {
            var c = db.Candidates.Single(a => a.ID == id);
            // For simplicity, we're allowing deleting of albums   
            // with existing orders We've set up OnDelete = Cascade   
            // on the Album->OrderDetails and Album->Carts relationships  
            db.DeleteObject(c);
            db.SaveChanges();
            return View("Deleted");
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

                    var c = from a in db.Candidates
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
                    var c = from a in db.Candidates
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
                    var c = from a in db.Candidates

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
        public ActionResult Test(int id)
        {
            var c = db.Candidates.Single(a => a.ID == id);
            var Tests = from t in t_db.Tests where t.valid == true select t;
            List<Test> MyList = Tests.ToList(); 
                
            if (MyList.Any())
            {
                

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
                var viewModel = new C_T_IndexViewModel
                {
                    CID = id,
                    TID = tids,
                    TName = tnames
                };
                return View(viewModel); 
            }
            else
            {
                return RedirectToAction("Error", "Marks", new { massage = "There is no valid test" });
            }

            
           

        }
        [AcceptVerbs(HttpVerbs.Post), Authorize(Roles = "Admin,Power_User,HR")]
         public ActionResult C_T(int cid, int? tests, DateTime? Date, int time)
        {
            if (tests != null)
            {
                string[] mytime = new string[] { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00" };
                String mystr = Date.ToString();
                String mtstr = mytime[time];
                //    mystr += mtstr;


                String date_isu = String.Format("{0:s}", DateTime.Now);

                string[] datespliter = mystr.Split('/');
                if (datespliter[0].Count() == 1)
                //Month
                {
                    String zero = "0";
                    zero += datespliter[0];
                    datespliter[0] = zero;

                }
                // Day
                if (datespliter[1].Count() == 1)
                {
                    String zero = "0";
                    zero += datespliter[1];
                    datespliter[1] = zero;

                }
                datespliter[2] = datespliter[2].Substring(0, 4);

                String my_f_time = datespliter[2];
                my_f_time += "-";
                my_f_time += datespliter[0];
                my_f_time += "-";
                my_f_time += datespliter[1];
                my_f_time += "T";
                my_f_time += mytime[time];

                C_T_R ct = new C_T_R();
                ct.C_ID = cid;
                ct.T_ID = (int) tests;
                ct.Date_Isu = DateTime.Parse(date_isu);
                ct.Date_to_Set = DateTime.Parse(my_f_time);
                ct.is_submitted = false;
                
                Test t = t_db.Tests.Single(a => a.ID == tests);
                Candidate c = db.Candidates.Single(a => a.ID == cid);
                string email = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).Email;
                TimeSpan diff = DateTime.Parse(my_f_time) - DateTime.Parse(date_isu);
                int days = diff.Days;


                /* NO Mail Server
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("no-replay@ise-ltd.com");
                    message.To.Add(new MailAddress(c.Email));
                    message.Bcc.Add(new MailAddress(email));
                    message.Subject = "Your Test in Internation Systems Engineering";
                    message.Body = "";
                    message.IsBodyHtml = false;
                    string bodyHtml = "<p>You are going to have the <strong>" + t.Name + " Test on " + DateTime.Parse(my_f_time) + " After " + days + " days from the date of this email</strong></br> <p> The test about " + t.Description + " and the test duration is " + t.Duration + " min </p>"
                        +"<br><br> your username is : " + c.Uname + " <br> and your password is : " + c.Password + "<br><br><br> Note: Date formate is Monthe/Day/Year Time";
                    using (AlternateView altView = AlternateView.CreateAlternateViewFromString(bodyHtml, new ContentType(MediaTypeNames.Text.Html)))
                    {
                        message.AlternateViews.Add(altView);
                        SmtpClient smtp = new SmtpClient("Ksaisefe01.ise.local");
                        smtp.Credentials = new NetworkCredential("ise\\isewb", "isewb01");
                        try
                        {
                            smtp.Send(message);*/
                            c_t_db.AddToC_T_R(ct);
                            c_t_db.SaveChanges();
                      /*  }
                        catch (Exception ex)
                        {
                            return RedirectToAction("Error", "Marks", new { massage = "Can not Link" });
                        }


                    }
                }*/
                
                return View("Linked");
            }
            else
            {
                return RedirectToAction("Error", "Marks", new { massage = "You have not select a test" });
            }
           

    }
}
    }
