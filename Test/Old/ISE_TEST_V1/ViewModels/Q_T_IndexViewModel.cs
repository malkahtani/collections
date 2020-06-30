using System;
using System.Collections.Generic;

namespace ISE_TEST_V1.ViewModels
{
    public class Q_T_IndexViewModel
    {
        public int QID { get; set; }
        public int QType { get; set; }
        public List<int> TID { get; set; }
        public List<String> TName { get; set; }
    }
}
