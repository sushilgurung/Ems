using Library.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation
{
    public interface IActivityService
    {
        void AddActivityLog(ActivityLog model);
    }
}
