using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISE_TEST_V1.Models
{
    public class MAnswer
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public int Q_ID { get; set; }
        public string Text { get; set; }
        public bool is_right { get; set; }
        public int A_Order { get; set; }
        public int value { get; set; }
    }
}
