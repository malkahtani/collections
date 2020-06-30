using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
namespace Attendance.Models
{
    public class TReports
    {
        private int compare(string first, string second)
        {

            DateTime f = DateTime.Parse(first);
            DateTime s = DateTime.Parse(second);

            return DateTime.Compare(f, s);
        }

        EventEntities Events = new EventEntities();
        UsersEntities Users = new UsersEntities();
        public List<TopV> toplist { get; set; }
        [Required(ErrorMessage = "Users are required")]
        public List<User_R> users_r { get; set; }
        TopV toadd = new TopV();
        DL L = new DL();

        IDevicesRepo _Drepo;
        IManagerRepo _Mrepo;

        public TReports(int mid, DateTime f, DateTime t, List<int> e, int num)
        {
            toplist = new List<TopV>();
            toplist.Add(toadd);
            users_r = new List<User_R>();
            List<LogL> mylog = new List<LogL>();
            _Drepo = new DevicesRepo();
            _Mrepo = new ManagersRepo();
            TimeSpan days = (t - f);
            int din = 0; int dout = 0;

            List<Devices> d = _Drepo.GetDevices().ToList();
            Attend Best = new Attend();
            Attend Worst = new Attend();
            Hours h = new Hours();
            decimal thours = 0;
            decimal hours = 0;
            decimal tmp = 0;
            decimal tsthours = 0;
            decimal tshours = 0;
            int maxindex = 0;
            int minindex = 0;
            List<decimal> temphours = new List<decimal>();
            EmpL employees = new EmpL();



            for (int i = 0; i < e.Count; i++)
            {
                thours = 0;
                tsthours = 0;


                DateTime tempdate = f;
                LogL thelog = new LogL();

                mylog.Add(thelog);

                int uu = e.ElementAt(i);
                string uid = Convert.ToString(uu).ToUpperInvariant();
                var us = (from u in Users.TB_USER where u.sUserID == uid select u).ToList();


                DateList my_att = new DateList();
                L.DataL.Add(my_att);

                for (int j = 0; j < days.TotalDays; j++)
                {
                    hours = 0;
                    tmp = 0;
                    tshours = 0;

                    Log logs = new Log();

                    bool isReg = false;
                    mylog.ElementAt(i).Li.Add(logs);
                    TimeSpan span = (tempdate - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
                    double date_from = span.TotalSeconds;
                    DateTime temp = tempdate.AddDays(1);
                    TimeSpan tempspan = (temp - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
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
                    var log = (from n in Events.TB_EVENT_LOG where n.nUserID == uu && n.nDateTime >= date_from && n.nDateTime < date_to && (n.nReaderIdn == din || n.nReaderIdn == dout) select n).OrderBy(c => c.nDateTime).ToList();
                   
                    List<INOUT> hourslog = new List<INOUT>();
                    for (int ww = 0; ww < log.Count; ww++)
                    {
                        if (log.ElementAt(ww).nReaderIdn == din)
                        {

                            
                            INOUT htemp = new INOUT(true, log.ElementAt(ww).nDateTime);
                            hourslog.Add(htemp);
                        }
                        else
                            if (log.ElementAt(ww).nReaderIdn == dout)
                            {
                                
                                INOUT htemp = new INOUT(false, log.ElementAt(ww).nDateTime);
                                hourslog.Add(htemp);
                            }

                    }



                    if (hourslog.Count > 0)
                    {
                        while ((hourslog.Count > 1) && (hourslog.ElementAt(0).InOut == false))
                        {
                           

                            hourslog.RemoveAt(0);
                            isReg = true;

                        }

                        while ((hourslog.Count > 1) && (hourslog.ElementAt((hourslog.Count - 1)).InOut == true))
                        {
                           

                            hourslog.RemoveAt((hourslog.Count - 1));
                            isReg = true;

                        }

                    }


                    hourslog.Reverse();


                    for (int n = hourslog.Count - 1; n > 0; n--)
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

                                   
                                    hourslog.RemoveAt(n);
                                    isReg = true;

                                }
                                else if (hourslog.ElementAt(n).InOut == false)
                                {
                                    
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
                        if (hourslog.ElementAt(n).InOut != hourslog.ElementAt(z).InOut)
                        {
                           

                            tmp = 0;
                            tmp = hourslog.ElementAt(z).timestamp - hourslog.ElementAt(n).timestamp;
                            tshours = tmp + tshours;
                            tsthours = tsthours + tmp;

                            hours = tshours / 3600;

                            thours = tsthours / 3600;
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
                    

                    Date_R day = new Date_R(String.Format("{0:d/M/yyyy , dddd  }", tempdate), hours, isReg, mylog.ElementAt(i).Li.ElementAt(j).InLog, mylog.ElementAt(i).Li.ElementAt(j).OutLog,"");
                    L.DataL.ElementAt(i).DaL.Add(day);

                   


                    tempdate = tempdate.AddDays(1);




                }

                h.H.Add(thours);
                Attend emp = new Attend(us.ElementAt(0).sUserName, uu, thours);
                employees.Li.Add(emp);
                String username = us.ElementAt(0).sUserName;
                
                maxindex = h.H.IndexOf(h.H.Max());
                minindex = h.H.IndexOf(h.H.Min());

                Worst = employees.Li.ElementAt(minindex);
                Best = employees.Li.ElementAt(maxindex);











                User_R temp_user = new User_R(username, uu, L.DataL.ElementAt(i).DaL, thours, Best, Worst);

                users_r.Add(temp_user);


            }
            temphours.AddRange(h.H);
            var maxfoundIndexes = new List<int>();
            var minfoundIndexes = new List<int>();
            for (int x = 0; x < num; x++)
            {

                for (int ii = h.H.IndexOf(temphours.Max()); ii > -1; ii = h.H.IndexOf(temphours.Max(), ii + 1))
                {
                    // for loop end when i=-1 ('a' not found)
                    if (maxfoundIndexes.Count <= num)

                    {
                        if (!maxfoundIndexes.Contains(ii))
                        {
                            maxfoundIndexes.Add(ii);
                        }
                    }
                }
                for (int ii = h.H.IndexOf(temphours.Min()); ii > -1; ii = h.H.IndexOf(temphours.Min(), ii + 1))
                {
                    if (minfoundIndexes.Count <= num)
                    {
                        if (!minfoundIndexes.Contains(ii))
                        {
                            minfoundIndexes.Add(ii);
                        }
                    }
                }
                
                var max = (from m in h.H where m == temphours.Max() select m).ToList();
                var min = (from m in h.H where m == temphours.Min() select m).ToList();
                                  
                    
                    for (int i = 0; i < max.Count; i++)
                    {
                      
                        temphours.RemoveAt(temphours.IndexOf(temphours.Max()));
                      
                    }
                
                    for (int i = 0; i < min.Count; i++)
                    {
                        
                        temphours.RemoveAt(temphours.IndexOf(temphours.Min()));
                       
                    }
                
            }
            for (int y = 0; y < num; y++)
            {

                Attend BestAtt = employees.Li.ElementAt(maxfoundIndexes.ElementAt(y));
                Attend WorstAtt = employees.Li.ElementAt(minfoundIndexes.ElementAt(y));
                toplist.ElementAt(0).Top.Add(BestAtt);
                toplist.ElementAt(0).Bot.Add(WorstAtt);
            }
        }


    }

}