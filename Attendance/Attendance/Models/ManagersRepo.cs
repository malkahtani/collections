using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Attendance.Models
{
    public class ManagersRepo : IManagerRepo
    {
        private List<Managers> allManagers;
        private XDocument ManagersData;

        public ManagersRepo()
        {
            allManagers = new List<Managers>();

            ManagersData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Managers.xml"));
            // Get item nodes
            var items = ManagersData.Descendants("item");

         /*  var Managers = from Manager in ManagersData.Descendants("item")
                           select new Managers(
                               int.Parse(Manager.Element("id").Value),
                               Manager.Element("UserName").Value,
                             int.Parse(Manager.Element("reportto").Value),
                            Manager.Descendants("employees").Select(s => int.Parse(s.Value)).ToList());*/
        
            //allManagers.AddRange(Managers.ToList<Managers>());
            var query =
                (from Manager in ManagersData.Descendants("item")
                select new
                {
                    id = int.Parse(Manager.Element("id").Value),
                    username = Manager.Element("UserName").Value,
                    reportto = int.Parse(Manager.Element("reportto").Value),
                    
                }).ToList();
            

            for (int i = 0; i < query.Count; i++)
            {
                List<int> emp = new List<int>();

                var myemp = (from p in ManagersData.Descendants("item")
 
                where int.Parse(p.Element("id").Value) == query.ElementAt(i).id
 
                from m in p.Descendants("employee")
                select m).ToList();


                for (int j = 0; j < myemp.Count; j++)
                {
                    emp.Add(int.Parse(myemp.ElementAt(j).Value));
                    
                }
                Managers employee = new Managers(query.ElementAt(i).id, query.ElementAt(i).username, query.ElementAt(i).reportto, emp);
                allManagers.Add(employee);

            }
       
        }
        public IEnumerable<Managers> GetManagers()
        {
            return allManagers;
        }

    }
}
