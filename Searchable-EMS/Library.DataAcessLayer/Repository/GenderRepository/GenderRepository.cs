using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public DatabaseEntities DbContext
        {
            get
            {
                return DatabaseEntities as DatabaseEntities;
            }
        }
    }
}
