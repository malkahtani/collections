using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class DateList
    {
        public List<Date_R> DaL { set; get; }
    
    public DateList(){
        DaL = new List<Date_R>();
    }
    }
}