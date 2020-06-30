using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISE_TEST_V1.Models
{
    public class MQuestion
    {
        public int ID { get; set; }
        public int Wight { get; set; }
        public short N_Answers { get; set; }
        public short Type { get; set; }
        public String Text { get; set; }
    }
}
