using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class LogL
    {
        public List<Log> Li { set; get; }
        
        public LogL()
        {

            Li = new List<Log>();
            
        }
    }
}