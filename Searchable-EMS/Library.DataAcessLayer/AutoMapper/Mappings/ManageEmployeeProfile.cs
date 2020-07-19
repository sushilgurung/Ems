using AutoMapper;
using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Entities;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer
{
    public class ManageEmployeeProfile : Profile
    {
        public ManageEmployeeProfile()
        {
            CreateMap<Employee, ManageEmployeeListViewModel>();
            CreateMap<ManageEmployeeListViewModel, Employee>();

            CreateMap<Employee, ManageEmployeeCreateViewModel>();
            CreateMap<ManageEmployeeCreateViewModel, Employee>();

            CreateMap<Employee, EmployeeDetailViewModel>();
            CreateMap<EmployeeDetailViewModel, Employee>();

            
        }

    }
}
