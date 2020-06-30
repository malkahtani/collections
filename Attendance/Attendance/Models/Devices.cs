using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
namespace Attendance.Models
{
    public class Devices
    {
        [Required(ErrorMessage = "Main Door in device's ID is required")]
        public int in_device { get; set; }
        [Required(ErrorMessage = "Main Door out device's ID is required")]
        public int out_device { get; set; }
        [Required(ErrorMessage = "Date the Main Door in device starting date is required")]
        public DateTime in_date_start { get; set; }
        [Required(ErrorMessage = "Date the Main Door in device ending date is required - type now for working device")]
        public DateTime in_date_stop { get; set; }
        [Required(ErrorMessage = "Date the Main Door out device starting date is required")]
        public DateTime out_date_start { get; set; }
        [Required(ErrorMessage = "Date the Main Door out device ending date is required - type now for working device")]
        public DateTime out_date_stop { get; set; }
        public Devices(){
            this.in_device = 0;
            this.out_device = 0;
            this.in_date_start = new DateTime(1970, 01, 01);
            this.in_date_stop = DateTime.Today;
            this.out_date_start = new DateTime(1970, 01, 01);
            this.out_date_stop = DateTime.Today;


        }
        public Devices(int ind, int oud, string in_date_start, string in_date_stop, string out_date_start, string out_date_stop)
        {
           


            this.in_device = ind;
            this.out_device = oud;
            
            if(in_date_start.Equals("now",StringComparison.InvariantCultureIgnoreCase)){
                this.in_date_start = DateTime.Today; 
            }else{
                this.in_date_start = Convert.ToDateTime(in_date_start);
            }
            
            if(in_date_stop.Equals("now",StringComparison.InvariantCultureIgnoreCase)){
                this.in_date_stop = DateTime.Today;
                
            }else{
                this.in_date_stop = Convert.ToDateTime(in_date_stop);
             
            }

            if(out_date_start.Equals("now",StringComparison.InvariantCultureIgnoreCase)){
                this.out_date_start = DateTime.Today;
                
            }else{
                this.out_date_start = Convert.ToDateTime(out_date_start); 
            }
            if(out_date_stop.Equals("now",StringComparison.InvariantCultureIgnoreCase)){
                this.out_date_stop = DateTime.Today;
            }else{
                this.out_date_stop = Convert.ToDateTime(out_date_stop); 
            }

        }
        
    }
}