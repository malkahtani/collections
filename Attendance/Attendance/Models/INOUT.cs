using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class INOUT
    {
        // true == In , false == Out
        public bool InOut { set; get; }
        public decimal timestamp { set; get; }
        public INOUT(bool inout, decimal ts)
        {
            this.InOut = inout;
            this.timestamp = ts;
        }
       
    }
}