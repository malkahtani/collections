using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Date_R
    {
        public String day { set; get; }
        public decimal hours { set; get; }
        
        public  bool CheckedInOrOut { set; get; }
        
        public List<string> CheckInLog { set; get; }
        public List<string> CheckoutLog { set; get; }
        public string Type { set; get; }
        public Date_R(String d, decimal h, bool cin, List<string> inLog, List<string> outLog, string t)
        {
            this.day = d;
            this.hours = h;
            
            this.CheckedInOrOut = cin;
            
            this.CheckInLog = inLog;
            this.CheckoutLog = outLog;
            this.Type = t;
        }

    }
}