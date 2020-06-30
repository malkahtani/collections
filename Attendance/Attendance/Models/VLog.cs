using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class VLog
    {
        public string log { set; get; }
        public int r { set; get; }
        public VLog()
        {
            this.log= "";
            this.r = 0;
        }
    }
}