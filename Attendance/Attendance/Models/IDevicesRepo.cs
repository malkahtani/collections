using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Models
{
    public interface IDevicesRepo
    {
        IEnumerable<Devices> GetDevices();
         void InsertDevice(Devices d);
         
          
    }
}
