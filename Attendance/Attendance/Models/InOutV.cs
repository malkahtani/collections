using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class InOutV
    {
        // true == In , false == Out
        public List<decimal> OutLog { set; get; }
        public List<decimal> InLog { set; get; }
        public InOutV()
        {
            this.InLog = new List<decimal>();
            this.OutLog = new List<decimal>();
        }

    }
}