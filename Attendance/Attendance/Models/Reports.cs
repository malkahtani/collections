using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
namespace Attendance.Models
{
    public class Reports
    {
        private int compare(string first, string second)
        {
            if (first == "Bypass The Entrance")
            {
                return 1;
            }
            if(second == "Bypass The Entrance"){
                return -1;
            }
            DateTime f = DateTime.Parse(first);
            DateTime s = DateTime.Parse(second);

            return DateTime.Compare(f, s);
        }

        EventEntities Events = new EventEntities();
        UsersEntities Users = new UsersEntities();
        LeaveEntities Leaves = new LeaveEntities();
        Leave_Type_Entities LeaveType = new Leave_Type_Entities();
        /*DateTime From { get; set; }
        DateTime To { get; set; }
        List<int> Emplyees { set; get; }*/
        [Required(ErrorMessage = "Users are required")]
        public List<User_R> users_r { get; set;}
       
        DL L = new DL();

        IDevicesRepo _Drepo;
        IManagerRepo _Mrepo;

        public Reports(int mid, DateTime f, DateTime t, List<int> e)
        {
            if (t == f)
            {
                f = f.AddDays(-1);

            }
            string ty = "";
            users_r = new List<User_R>();
            List<LogL> mylog = new List<LogL>();   
            _Drepo= new DevicesRepo();
            _Mrepo = new ManagersRepo();
            
            //t = t.AddDays(1);
            TimeSpan days = (t - f);
            int din =0;int dout = 0;
            
            List<Devices> d = _Drepo.GetDevices().ToList();
            Attend Best = new Attend();
            Attend Worst = new Attend();
            Hours h = new Hours();
            decimal thours=0;
            decimal hours = 0;
            decimal tmp = 0;
            decimal tsthours = 0;
            decimal tshours = 0;
            int maxindex =0;
            int minindex=0;
            EmpL employees = new EmpL();
            
           
            
            for (int i = 0; i < e.Count; i++)
             {
                 thours = 0;
                    tsthours = 0;
                    
                
                DateTime tempdate = f;
                LogL thelog = new LogL();
                
                mylog.Add(thelog);
                
                int uu = e.ElementAt(i);
                string  uid = Convert.ToString(uu).ToUpperInvariant();
                var us = (from u in Users.TB_USER where u.sUserID == uid select u).ToList();
                var empleave = (from l in Leaves.Leaves where (l.Emp_Id == uu) && (l.DFrom >= f && l.DTo <= t) select l).ToList();
               
                DateList my_att = new DateList();
                L.DataL.Add(my_att);
                
                    for(int j=0; j<= days.TotalDays; j++){
                         hours = 0;
                         tmp = 0;
                         tshours = 0;
                         bool is_leave_day = false;
                        Log logs = new Log();
                        
                        bool isReg = false;
                        mylog.ElementAt(i).Li.Add(logs);
                    TimeSpan span = (tempdate - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
                    double date_from = span.TotalSeconds;
                    DateTime temp = tempdate.AddDays(1);
                    TimeSpan tempspan = (temp - new DateTime(1970, 1, 1, 0, 0, 0, 0,DateTimeKind.Local));
                    double date_to = tempspan.TotalSeconds;


                     //Get the devices
                    for (int x = 0; x < d.Count; x++)
                    {
                        if (d.ElementAt(x).in_date_start.Ticks <= tempdate.Ticks && d.ElementAt(x).in_date_stop.Ticks >= temp.Ticks &&
                            d.ElementAt(x).out_date_start.Ticks <= tempdate.Ticks && d.ElementAt(x).out_date_stop.Ticks >= temp.Ticks)
                        {
                            din = d.ElementAt(x).in_device;
                            dout = d.ElementAt(x).out_device;
                            
                        }
                    }
                    var log = (from n in Events.TB_EVENT_LOG where (n.nUserID == uu) && (n.nDateTime >= date_from && n.nDateTime <= date_to) && (n.nReaderIdn == din || n.nReaderIdn == dout) select n).OrderBy(c => c.nDateTime).ToList();
                    // var log1 = (from n in Events.TB_EVENT_LOG where n.nUserID == uu && n.nDateTime >= date_from && n.nDateTime < date_to && n.nReaderIdn == dout select n).OrderBy(c => c.nDateTime).ToList();
                   /* for (int b = 0; b < empleave.Count; b++)

                    {
                        empleave.ElementAt(b).DTo = empleave.ElementAt(b).DTo.AddDays(1);
                    }*/
                        for (int b = 0; b < empleave.Count; b++)
                    {
                        
                        if ((tempdate >= empleave.ElementAt(b).DFrom) && (temp <= empleave.ElementAt(b).DTo.AddDays(1)))
                        {
                        is_leave_day = true;
                        }
                    }
                     List<INOUT> hourslog = new List<INOUT>();
                     for (int ww = 0; ww < log.Count; ww++)
                     {
                         if (log.ElementAt(ww).nReaderIdn == din)
                         {

                            DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                             inlog = inlog.AddSeconds(log.ElementAt(ww).nDateTime);
                              String inl = String.Format("{0:h:mm tt }", inlog);
                             
                             mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                             INOUT htemp = new INOUT(true, log.ElementAt(ww).nDateTime);
                             hourslog.Add(htemp);
                         }else
                             if (log.ElementAt(ww).nReaderIdn == dout)
                             {
                                DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                               outlog = outlog.AddSeconds(log.ElementAt(ww).nDateTime);
                               String oul = String.Format("{0:h:mm tt }", outlog);
                               mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);
                                 INOUT htemp = new INOUT(false, log.ElementAt(ww).nDateTime);
                                 hourslog.Add(htemp);
                             }

                     }
                    

                                          
                        if(hourslog.Count >0){
                         while((hourslog.Count >1) && (hourslog.ElementAt(0).InOut == false))
                         {
                            /* TB_EVENT_LOG mylog1 = log.Find(w => w.nDateTime == hourslog.ElementAt(0).timestamp);

                             
                             
                             DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                             string inl = "Bypass The Entrance";
                             mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                             
                             DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                             outlog = outlog.AddSeconds(mylog1.nDateTime);
                             String oul = String.Format("{0:h:mm tt }", outlog);
                             mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);*/

                             hourslog.RemoveAt(0);
                             isReg = true;

                         }

                         while((hourslog.Count >1) && (hourslog.ElementAt((hourslog.Count - 1)).InOut == true))
                         {
                          /*   TB_EVENT_LOG mylog1 = log.Find(w => w.nDateTime == hourslog.ElementAt(0).timestamp);

                             DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                             inlog = inlog.AddSeconds(mylog1.nDateTime);
                             String inl = String.Format("{0:h:mm tt }", inlog);
                             mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                             
 
                             DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                             string oul = "Bypass The Exit";
                             mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);*/

                             hourslog.RemoveAt((hourslog.Count - 1));
                             isReg = true;

                         }

                     }

                    
                     hourslog.Reverse();


                            for (int n = hourslog.Count -1; n > 0; n--)
                            {
                                int z = n;
                                if (n > 0)
                                {
                                     z = n - 1;
                                }
                                if (!(z == 0))
                                {
                                    if (hourslog.ElementAt(n).InOut == hourslog.ElementAt(z).InOut)
                                    {
                                        if (hourslog.ElementAt(n).InOut == true)
                                        {
                                            // 2 check-in

                                           /* TB_EVENT_LOG mylog1 = log.Find(w => w.nDateTime == hourslog.ElementAt(z).timestamp);

                                            DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                            inlog = inlog.AddSeconds(mylog1.nDateTime);
                                            String inl = String.Format("{0:h:mm tt }", inlog);
                                            mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                                            
 
                                            DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                            string oul = "Bypass The Exit";
                                            mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);*/
                                            hourslog.RemoveAt(n);
                                            isReg = true;
                                            
                                        } else if (hourslog.ElementAt(n).InOut == false)
                                        {
                                           /*TB_EVENT_LOG mylog1 = log.Find(w => w.nDateTime == hourslog.ElementAt(n).timestamp);

                                            DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                            string inl = "Bypass The Entrance";
                                            mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                                            

                                            DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                            outlog = outlog.AddSeconds(mylog1.nDateTime);
                                            String oul = String.Format("{0:h:mm tt }", outlog);
                                            mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);*/
                                            // 2 check-out
                                            hourslog.RemoveAt(z);
                                            isReg = true;
                                        }
                                        
                                        
                                    }
                                }
                            }
                            for (int n = hourslog.Count - 1; n > 0; n--)
                            {
                                int z = n;
                                if (n > 0)
                                {
                                    z = n - 1;
                                }
                                if(hourslog.ElementAt(n).InOut != hourslog.ElementAt(z).InOut) 
                                {
                                  /*  TB_EVENT_LOG mylog1 = log.Find(w => w.nDateTime == hourslog.ElementAt(n).timestamp);
                                    TB_EVENT_LOG mylog2 = log.Find(w => w.nDateTime == hourslog.ElementAt(z).timestamp);

                                    DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                    inlog = inlog.AddSeconds(mylog1.nDateTime);
                                    String inl = String.Format("{0:h:mm tt }", inlog);
                                    mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                                    // else if 
                                    DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                     outlog = outlog.AddSeconds(mylog2.nDateTime);
                                     String oul = String.Format("{0:h:mm tt }", outlog);
                                     mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);
                                    */
                                    if (!is_leave_day)
                                    {
                                        tmp = 0;
                                        tmp = hourslog.ElementAt(z).timestamp - hourslog.ElementAt(n).timestamp;
                                        tshours = tmp + tshours;
                                        tsthours = tsthours + tmp;

                                        hours = tshours / 3600;

                                        thours = tsthours / 3600;
                                    }
                                   n--;
                                 
                                   
                                    
                                }
                                else
                                {
                                   
                                    if (z != 0)
                                    {
                                        isReg = true;
                                    }
                                }
                            }
                            if (is_leave_day)
                            {
                                hours = 8;
                                thours = thours + 8;
                            }

                       /*     int ii, jj;
                            int min;
                            string tempval1, tempval2;
                            string inlc = "Bypass The Entrance";
                             string oulc = "Bypass The Exit";
                            for (ii = 0; ii < mylog.ElementAt(i).Li.ElementAt(j).InLog.Count - 1; ii++)
                            {
                                min = ii;

                                for (jj = i + 1; jj < mylog.ElementAt(i).Li.ElementAt(j).InLog.Count; jj++)
                                {
                                    if ((mylog.ElementAt(i).Li.ElementAt(j).InLog[jj] != inlc) || (mylog.ElementAt(i).Li.ElementAt(j).InLog[jj] != oulc) && (mylog.ElementAt(i).Li.ElementAt(j).InLog[min] != inlc) || (mylog.ElementAt(i).Li.ElementAt(j).InLog[min] != oulc))
                                    {
                                        if (compare(mylog.ElementAt(i).Li.ElementAt(j).InLog[jj], mylog.ElementAt(i).Li.ElementAt(j).InLog[min]) < 0)
                                        {
                                            min = jj;
                                        }
                                    }
                                    
                                }

                                tempval1 = mylog.ElementAt(i).Li.ElementAt(j).InLog[ii];
                                tempval2 = mylog.ElementAt(i).Li.ElementAt(j).OutLog[ii];
                                mylog.ElementAt(i).Li.ElementAt(j).InLog[ii] = mylog.ElementAt(i).Li.ElementAt(j).InLog[min];
                                mylog.ElementAt(i).Li.ElementAt(j).OutLog[ii] = mylog.ElementAt(i).Li.ElementAt(j).OutLog[min];
                                mylog.ElementAt(i).Li.ElementAt(j).InLog[min] = tempval1;
                                mylog.ElementAt(i).Li.ElementAt(j).OutLog[min] = tempval2;
                            }

                          /* string inlc = "Bypass The Entrance";
                             string oulc = "Bypass The Exit";
                             for (int cc = 0; cc < mylog.ElementAt(i).Li.ElementAt(j).InLog.Count; cc++)
                             {
                                 string com = "";
                                 bool usin = false;
                                 string inforcom = mylog.ElementAt(i).Li.ElementAt(j).InLog.ElementAt(cc);
                                 string outforcom = mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(cc);
                                 if (inforcom == inlc || inforcom == oulc)
                                 {
                                     com = outforcom;
                                     usin = false;
                                 }
                                 else
                                 {
                                     usin = true;
                                     com = inforcom;

                                 }
                                 for(int xx = mylog.ElementAt(i).Li.ElementAt(j).InLog.Count - 1; xx > cc+1 ; xx--)
                                 {
                                     string temp1 = mylog.ElementAt(i).Li.ElementAt(j).InLog.ElementAt(xx);
                                     string temp2 = mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(xx);
                                     if (usin)
                                     {
                                         if ((mylog.ElementAt(i).Li.ElementAt(j).InLog.ElementAt(xx) != inlc) && mylog.ElementAt(i).Li.ElementAt(j).InLog.ElementAt(xx) != oulc)
                                         {
                                             if (compare(mylog.ElementAt(i).Li.ElementAt(j).InLog.ElementAt(xx), com) < 0)
                                             {
                                                 mylog.ElementAt(i).Li.ElementAt(j).InLog[cc] = temp1;
                                                 mylog.ElementAt(i).Li.ElementAt(j).OutLog[cc] = temp2;
                                                 mylog.ElementAt(i).Li.ElementAt(j).InLog[xx] = inforcom;
                                                 mylog.ElementAt(i).Li.ElementAt(j).OutLog[xx] = outforcom;


                                             }
                                         }
                                        
                                     }
                                     else
                                     {
                                         if ((mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(xx) != inlc) && (mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(xx) != oulc))
                                         {
                                             if ((compare(com,mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(xx)) < 0) || (compare(com, mylog.ElementAt(i).Li.ElementAt(j).OutLog.ElementAt(xx)) == 0))
                                             {
                                                 mylog.ElementAt(i).Li.ElementAt(j).InLog[cc] = temp1;
                                                 mylog.ElementAt(i).Li.ElementAt(j).OutLog[cc] = temp2;
                                                 mylog.ElementAt(i).Li.ElementAt(j).InLog[xx] = inforcom;
                                                 mylog.ElementAt(i).Li.ElementAt(j).OutLog[xx] = outforcom;


                                             }
                                         }
                                        
                                        
                
                                     }

                                 }
                             }*/
                            if (!is_leave_day && hours <= 0)
                            {
                                ty = "Absence";
                                if((tempdate.DayOfWeek == DayOfWeek.Saturday) || (tempdate.DayOfWeek == DayOfWeek.Friday))
                                {
                                    ty= "Weekend";
                                }
                            }
                            else if (is_leave_day)
                            {
                                ty = "Leave";
                            }
                            else if (hours > 0)
                            {
                                ty = "Normal";
                            }
                            Date_R day = new Date_R(String.Format("{0:d/M/yyyy , dddd  }", tempdate), hours, isReg, mylog.ElementAt(i).Li.ElementAt(j).InLog, mylog.ElementAt(i).Li.ElementAt(j).OutLog,ty);
                     L.DataL.ElementAt(i).DaL.Add(day);
                          
                  /*  if (log.Count == log1.Count)
                    {
                        decimal hours = 0;
                        decimal tmp = 0;
                    
                        for (int w = 0; w < log.Count; w++)
                        {
                            if (log.ElementAt(w).nDateTime < log1.ElementAt(w).nDateTime)
                            {
                                tmp = log1.ElementAt(w).nDateTime - log.ElementAt(w).nDateTime;
                                hours = hours + tmp;
                                hours = hours / 3600;
                                thours = thours + hours;
                                
                                isReg = false;
                            }
                            else
                            {
                                isReg = true;
                            }

                           DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                           inlog = inlog.AddSeconds(log.ElementAt(w).nDateTime);
                           DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                           outlog = outlog.AddSeconds(log1.ElementAt(w).nDateTime);
                            String inl = String.Format("{0:h:mm tt }", inlog);
                            String oul = String.Format("{0:h:mm tt }", outlog);
                            mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                            mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);
                           
                            
                           
                        }
                        
                        Date_R day = new Date_R(String.Format("{0:d/M/yyyy , dddd  }", tempdate), hours, isReg, mylog.ElementAt(i).Li.ElementAt(j).InLog, mylog.ElementAt(i).Li.ElementAt(j).OutLog);
                        L.DataL.ElementAt(i).DaL.Add(day);
                        
                        
                        
                    }*/
                  /*  else if (log.Count > log1.Count)
                    {
                        decimal hours = 0;
                 
                        for (int w = 0; w < log.Count; w++)
                        {

                            DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            inlog = inlog.AddSeconds(log.ElementAt(w).nDateTime);
                            String inl = String.Format("{0:h:mm tt }", inlog);
                            mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                        }
                        for(int w = 0; w < log1.Count ; w++)
                        {
                            DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            outlog = outlog.AddSeconds(log1.ElementAt(w).nDateTime);
                            String oul = String.Format("{0:h:mm tt }", outlog);
                            mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul); 
                        }
                            
                               
                               Date_R day = new Date_R(String.Format("{0:d/M/yyyy , dddd }", tempdate), hours, true, mylog.ElementAt(i).Li.ElementAt(j).InLog, mylog.ElementAt(i).Li.ElementAt(j).OutLog);
                               L.DataL.ElementAt(i).DaL.Add(day);

                        
                         } */
                    
                  /*  else if(log1.Count > log.Count){

                              
                              decimal hours = 0;
                        for (int w = 0; w < log.Count; w++)
                        {

                            DateTime inlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            inlog = inlog.AddSeconds(log.ElementAt(w).nDateTime);
                            String inl = String.Format("{0:h:mm tt }", inlog);
                            mylog.ElementAt(i).Li.ElementAt(j).InLog.Add(inl);
                        }
                        for (int w = 0; w < log1.Count; w++)
                        {
                            DateTime outlog = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            outlog = outlog.AddSeconds(log1.ElementAt(w).nDateTime);
                            String oul = String.Format("{0:h:mm tt }", outlog);
                            mylog.ElementAt(i).Li.ElementAt(j).OutLog.Add(oul);
                        }
                        Date_R day = new Date_R(String.Format("{0:d/M/yyyy , dddd }", tempdate), hours, true, mylog.ElementAt(i).Li.ElementAt(j).InLog, mylog.ElementAt(i).Li.ElementAt(j).OutLog);
                        L.DataL.ElementAt(i).DaL.Add(day);
                         }*/
                    
                    



                    


                    tempdate = tempdate.AddDays(1);
                   
                    
                    
                   
                }

                    h.H.Add(thours);
                    Attend emp = new Attend(us.ElementAt(0).sUserName, uu, thours);
                    employees.Li.Add(emp);    
                    String username = us.ElementAt(0).sUserName;
                    decimal maxhours = 
                    maxindex = h.H.IndexOf(h.H.Max());
                    minindex = h.H.IndexOf(h.H.Min());
                   
                    Worst = employees.Li.ElementAt(minindex);
                    Best = employees.Li.ElementAt(maxindex);
                                      
                                    

                                   
                              

                            
                        
                    
               

                    User_R temp_user = new User_R(username, uu, L.DataL.ElementAt(i).DaL, thours, Best, Worst);
                     
                    users_r.Add(temp_user);

                
            }
        }

    
    }

}