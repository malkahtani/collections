using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISE_TEST_V1.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace ISE_TEST_V1.Controllers
{
    public class C_T_RController : Controller
    {
        //
        // GET: /C_T_R/
        Can_TestsEntities db = new Can_TestsEntities();
        Tests_Entities t_db = new Tests_Entities();
        Candidate1_Entities c_db = new Candidate1_Entities();

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Page(int id)
        {
            int PAGE_SIZE = 20;
            var Candidates = from q in db.C_T_R where q.is_submitted == false select q;
            List<C_T_R> c1 = Candidates.ToList();
            PagedList<C_T_R> data = new PagedList<C_T_R>(c1, id, PAGE_SIZE);
            return View(data);
        }
        [Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Delete(int id)
        {
            C_T_R c = db.C_T_R.Single(a => a.ID == id); return View(c);
        }

        [HttpPost, Authorize(Roles = "Admin,Power_User,HR")]
        public ActionResult Delete(int id, string confirmButton)
        {
            var c = db.C_T_R.Single(a => a.ID == id);
            int tests = c.T_ID;
            int cid = c.C_ID;
            string my_f_time = c.Date_to_Set.ToString();
            
            
            Test t = t_db.Tests.Single(a => a.ID == tests);
            Candidate can = c_db.Candidates.Single(a => a.ID == cid);
            string email = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).Email;
           



            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("no-replay@ise-ltd.com");
                message.To.Add(new MailAddress(can.Email));
                message.Bcc.Add(new MailAddress(email));
                message.Subject = "Your Test in Internation Systems Engineering Has been cancelled";
                message.Body = "";
                message.IsBodyHtml = false;
                string bodyHtml = "<p>You were scheduled to have the <strong>" + t.Name + " Test on " + DateTime.Parse(my_f_time) + " but this has been cancelled and you will be notified for the next schedule</strong></br>";

                using (AlternateView altView = AlternateView.CreateAlternateViewFromString(bodyHtml, new ContentType(MediaTypeNames.Text.Html)))
                {
                    message.AlternateViews.Add(altView);
                    SmtpClient smtp = new SmtpClient("Ksaisefe01.ise.local");
                    smtp.Credentials = new NetworkCredential("ise\\isewb", "isewb01");
                    try
                    {
                        smtp.Send(message);
                        db.DeleteObject(c);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Error", "Marks", new { massage = "Can not send cancellation email, Appointment not deleted." });
                    }
                    
                    return View("Deleted");
                }
            }
        }
    }
}
