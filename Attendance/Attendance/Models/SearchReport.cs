using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class SearchReports
    {
        public DateTime f { set; get; }
        public DateTime t { set; get; }
        [Required(ErrorMessage = "You have to select at least one employee")]
        public List<int> id { set; get; }
        public SearchReports(DateTime fr,DateTime tt, List<int> id)
        {
            this.id = id;
            this.f = fr;
            this.t = tt;
        }
        public SearchReports()
        {
            this.id = new List<int>();
            this.f = DateTime.Now;
            this.t = DateTime.Now;
        }
    }
}