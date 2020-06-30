using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class EmpL
    {
        public List<Attend> Li { set; get; }

        public EmpL()
        {

            Li = new List<Attend>();

        }
    }
}