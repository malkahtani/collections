using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Attendance.Models
{
    public class Managers
    {

        public int id { get; set; }
        public string username { get; set; }
        [Required(ErrorMessage = "ReportTO is required")]
        public int reportto { get; set; }
        [Required(ErrorMessage = "Emplyee is required")]
        public List<int> employees { get; set; }

        public Managers()
        {
            this.id = 0;
            this.username = "";
            this.reportto = 0;
            this.employees = null;

        }
        public Managers(int id, string username, int reportto, List<int> employees)
        {
            this.id = id;
            this.username = username;
            this.reportto = reportto;
            this.employees = employees;

        }
    }
}