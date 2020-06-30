using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ISE_TEST_V1.Models
{
    public class Roles
    {
        Roles_Manager_Entities db = new Roles_Manager_Entities();
        TestM_Role_Entities db1 = new TestM_Role_Entities();
        TestE_Role_Entities db2 = new TestE_Role_Entities();

        public Boolean is_role_manager(String role, String username)
        {
            var is_m = from r in db.Roles_Manager where ((r.RoleName == role) && (r.UserName == username)) select r;

            if (is_m.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean add_role_manager(String role, String username)
        {
            Roles_Manager m = new Roles_Manager();
            m.UserName = username;
            m.RoleName = role;

            db.AddToRoles_Manager(m);
            db.SaveChanges();
            
                return true;
            
        }
        public Boolean testM_in_role(String role, int Test_ID)
        {
            var is_m = from t in db1.TestM_Role where ((t.RoleName == role) && (t.Test_ID == Test_ID)) select t;
            if (is_m.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean add_testM_role(String role, int Test_ID)
        {
            TestM_Role m = new TestM_Role();
            m.Test_ID = Test_ID;
            m.RoleName = role;

            db1.AddToTestM_Role(m);
            db1.SaveChanges();

            return true;
            
        }
        public Boolean testE_in_role(String role, int Test_ID)
        {
            var is_m = from t in db1.TestM_Role where ((t.RoleName == role) && (t.Test_ID == Test_ID)) select t;
            if (is_m.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean add_testE_role(String role, int Test_ID)
        {
            TestM_Role m = new TestM_Role();
            m.Test_ID = Test_ID;
            m.RoleName = role;

            db1.AddToTestM_Role(m);
            db1.SaveChanges();

            return true;

        }

        public String get_role(String username)
        {
            string ro="";
           string[] roles = System.Web.Security.Roles.GetRolesForUser(username);
           for (int i = 0; i < roles.Count(); i++)
           {
               if (is_role_manager(roles[i], username))
               {
                  
                   
                   ro = roles[i];
                   break;
               }
           }
           return ro;
        }
    }
}