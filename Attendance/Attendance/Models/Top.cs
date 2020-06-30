using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Top
    {
        public DateTime f { set; get; }
        public DateTime t { set; get; }
        public List<int> id { set; get; }
        
        public int num { set; get; }
        public Top(DateTime fr, DateTime tt, List<int> id, int i)
        {
            this.id = id;
            this.f = fr;
            this.t = tt;
            this.num = i;
        }
        public Top()
        {
            this.id = new List<int>();
            this.f = DateTime.Now;
            this.t = DateTime.Now;
            this.num = 1; 
        }
    }
}