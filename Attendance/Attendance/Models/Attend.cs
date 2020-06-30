using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Attend
    {
        public string name { set; get; }
        public int id {set; get;}
        public decimal hours { set; get; }
        public Attend(string name, int id, decimal h)
        {
            this.id = id;
            this.name = name;
            this.hours = h;
        }
        public Attend()
        {
            this.id = 0;
            this.name = "";
            this.hours = 0;
        }
    }
}