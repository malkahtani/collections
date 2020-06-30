using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class TopV
    {
        public List<Attend> Top { set; get; }
        public List<Attend> Bot { set; get; }
        public TopV()
        {
            Top = new List<Attend>();
            Bot = new List<Attend>();
        }
    }
}