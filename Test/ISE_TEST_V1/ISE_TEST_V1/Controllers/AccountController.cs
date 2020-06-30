using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ISE_TEST_V1.Models;
using System.Collections.ObjectModel;

namespace ISE_TEST_V1.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        Roles_Manager_Entities db = new Roles_Manager_Entities();

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }
        // **************************************
        // URL: /Account/AddUser
        //***************************************
       public ActionResult AddUser()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            ISE_TEST_V1.Models.AddUserModel m = new ISE_TEST_V1.Models.AddUserModel();
            return View(m);
        }
       public ActionResult role_add(string massage)
       {


           ViewData["add"] = massage;

           return View();




       }
        public ActionResult New_E_Role(){
            return View();
        }
        [HttpPost]
        public ActionResult New_E_Role(string Role)
        {
            System.Web.Security.Roles.CreateRole(Role);

            return RedirectToAction("role_add", "Account", new { massage = "The Role " + Role + " has been added" });
            
        }
        public ActionResult New_M_Role()
        {
            return View();
        }
        public ActionResult New_M_Role(string Role)
        {
            System.Web.Security.Roles.CreateRole(Role);
            return RedirectToAction("role_add", "Account", new { massage = "The Role " + Role + " has been added" });

        }

        
        [HttpPost]
        public ActionResult AddUser(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email, model.Active_User, model.Role);

                if (createStatus == MembershipCreateStatus.Success)
                {
                  //  FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

       
        public ActionResult RM(int id)
        {
            int totalUsers;
           // int page = Int32.Parse(page1);
            MembershipUser[] users;
            //String[] roles =  System.Web.Security.Roles.GetAllRoles();
            MembershipUserCollection userscol = System.Web.Security.Membership.GetAllUsers(id-1, 20, out totalUsers);
            if (totalUsers <= 20 && id == 1)
            {
                users = new MembershipUser[totalUsers];
                userscol.CopyTo(users, 0);
            }
            else if (totalUsers <= 20 && id > 1)
            {
                users = null;
            }
            else
            {
                users = new MembershipUser[19];
                userscol.CopyTo(users, 0);
            }


           // double pp = totalUsers / 20;

            
            RM rm = new RM();
            //rm.Roles = roles;
            rm.Users = users;

            rm.pages = totalUsers / 20 + (totalUsers % 20 > 0 ? 1 : 0);
            
            rm.currentpage = id;
            
            return View(rm);
        }
        public ActionResult ADDRole(String username)
        {
            String[] roles = System.Web.Security.Roles.GetAllRoles();

            Dictionary<String, String> role = new Dictionary<string, string>();



            ur userr = new ur();

            for (int i = 0; i < roles.Count(); i++)
            {
                MyRoles mr = new MyRoles();

                role.Add(roles.ElementAt(i).ToString(), roles.ElementAt(i).ToString());
            }
            
            userr.username = username;
            userr.rs = role;
            return View(userr);
        }

        [HttpPost]
        public ActionResult Added(FormCollection collection)
        {
            addedr r = new addedr();
            r.role = collection["rs.Keys"];
            r.username = collection["username"];
            String[] users = System.Web.Security.Roles.GetUsersInRole(r.role);
            if (Array.Exists(users, s => s.Equals(r.username)))
            {

                //System.Web.Security.Roles.AddUserToRole(r.username, r.role);
                Roles_Manager rm = new Roles_Manager();
                rm.RoleName = r.role;
                rm.UserName = r.username;
                db.AddToRoles_Manager(rm);
                db.SaveChanges();
                ViewData["com"] = "Has been Add as a Manager to the Role";

                return View(r);
            }
            else
            {
                ViewData["com"] = "not in the role";
                return View(r);
            }
            
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
