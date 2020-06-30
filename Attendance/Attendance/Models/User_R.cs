using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class User_R
    {
        public string name { set; get; }
        public int id { set; get; }
        public decimal total_hours { set; get; }
        public List<Date_R> attendnace { set; get; }
        public Attend Batt { set; get; }
        public Attend Watt { set; get; }

        public User_R(string n, int id, List<Date_R> att, decimal th, Attend B, Attend W)
        {
            this.name = n;
            this.id = id;
            this.total_hours = th;
            this.attendnace = att;
            this.Batt = B;
            this.Watt = W;

        }
    }
}