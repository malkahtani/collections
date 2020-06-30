using System;
using System.Collections.Generic;
using ISE_TEST_V1.Models;

namespace ISE_TEST_V1.ViewModels { 
    public class MarksIndexView { 
        public List<C_T_R> ctr {set; get;}
        public List<Test> test { set; get; }
        public List<Candidate> can { set; get; }

        public List<List_AL> answers_log { set; get; }

            
    }
}