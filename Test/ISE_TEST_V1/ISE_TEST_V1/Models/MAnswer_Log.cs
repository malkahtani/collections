using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISE_TEST_V1.Models
{
    public class MAnswer_Log
    {
        public int ID { get; set; }
        public int C_T_R_ID { get; set; }
        public int PID { get; set; }
        public int Q_ID { get; set; }
        public int A_ID { get; set; }
        public string Text { get; set; }
        public bool is_right { get; set; }
        public int A_Order { get; set; }
        public int value { get; set; }
        public DateTime Date_Submitted { get; set; }
        public bool is_marked { get; set; }
        public bool need_rev { get; set; }
        public bool is_wrong { get; set; }
        public bool is_answered { get; set; }



    }
}
