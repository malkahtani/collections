using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Models
{
    public interface IManagerRepo
    {
        IEnumerable<Managers> GetManagers();
      /*  Managers GetManagerByID(int id);
        void InsertManager(Managers billing);
        void DeleteManager(int id);
        void EditManager(Managers billing);
        */
    }
}
