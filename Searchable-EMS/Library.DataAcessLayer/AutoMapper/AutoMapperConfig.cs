using AutoMapper;
using Library.DataAcessLayer.Entities;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ManageEmployeeProfile>();
            });
            // Mapper.Configuration.AssertConfigurationIsValid();
            //Mapper.AssertConfigurationIsValid();
        }
    }
}
