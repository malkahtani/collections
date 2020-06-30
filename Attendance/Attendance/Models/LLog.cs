using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class LLog
    {
        public List<LogL> mylog  { set; get; }
        private static LLog instance;

        private LLog()
        {

            mylog = new List<LogL>();
            
        }
        public static LLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LLog();
                }
                return instance;
            }
        }

    }
}