using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISE_TEST_V1.Models
{
    public class C_T_R_Sup
    {
        Tests_Entities t_db = new Tests_Entities();
        Candidate1_Entities c_db = new Candidate1_Entities();
        
        public Candidate get_can(int id){
        Candidate c = c_db.Candidates.Single(a => a.ID == id);
            return c;
        }
        public Test get_test(int id)
        {
            Test c = t_db.Tests.Single(a => a.ID == id);
            return c;
        }
    }
}