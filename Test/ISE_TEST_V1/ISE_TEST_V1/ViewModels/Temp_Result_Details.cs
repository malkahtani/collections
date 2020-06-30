using System;
using System.Collections.Generic;
using ISE_TEST_V1.Models;

namespace ISE_TEST_V1.ViewModels
{
    public class Temp_Reesult_Details
    {
        public Candidate c { get; set; }
        public List<Test> t { get; set; }
        public List<Temp_Result> t_r { get; set; }
        public List<Result> tt_r { get; set; }
        public List<bool> ind { get; set; }
        
    }
}
