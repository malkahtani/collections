using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Attendance.Models
{
    public class DevicesRepo : IDevicesRepo
    {
        private List<Devices> allDevices;
        private XDocument DevicesData;

        public DevicesRepo()
        {
            allDevices = new List<Devices>();

            DevicesData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Devices.xml"));
            // Get item nodes
            var items = DevicesData.Descendants("Device");

            var Devices = from Device in DevicesData.Descendants("Device")
                          select new Devices(
                               int.Parse(Device.Element("in").Value),
                             int.Parse(Device.Element("out").Value),
                           Device.Element("infrom").Value.ToString(),
                           Device.Element("into").Value.ToString(),
                           Device.Element("outfrom").Value.ToString(),
                           Device.Element("outto").Value.ToString());
            allDevices.AddRange(Devices.ToList<Devices>());

        }
        public IEnumerable<Devices> GetDevices()
        {
            return allDevices;
        }

        public void InsertDevice(Devices d)
        {

            DevicesData.Root.Add(new XElement("Device", new XElement("in", d.in_device), new XElement("out", d.out_device),
                new XElement("infrom", d.in_date_start.ToShortDateString()), new XElement("into", d.in_date_stop.ToShortDateString()), new XElement("outfrom", d.out_date_start.ToShortDateString()),
                new XElement("outto", d.out_date_stop.ToShortDateString())));

            DevicesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Devices.xml"));
        }
    }
}
