using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Log
    {
        public List<string> InLog {set; get;}
        public List<string> OutLog {set; get;}
        public Log()
        {
            InLog = new List<string>();
            OutLog = new List<string>();
        }
    }
}