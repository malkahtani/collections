using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Hours
    {
        public List<decimal> H { set; get; }
        public Hours()
        {
            H = new List<decimal>();
        }
    }
}