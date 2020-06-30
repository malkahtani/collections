using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class DL
    {
        public List<DateList> DataL { set; get; }
        public DL()
        {
            DataL = new List<DateList>();
        }
    }
}