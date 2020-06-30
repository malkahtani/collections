using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Attendance
    {
        List<User_R> my_emplyee { set; get; }
        public Attendance()
        {
            my_emplyee = new List<User_R>();
        }
    }
}