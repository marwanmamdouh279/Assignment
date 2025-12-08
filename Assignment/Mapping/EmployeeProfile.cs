using Assignment.DTOs;
using Assignment_DataAccesslayer.Entities;
using AutoMapper;

namespace Assignment.Mapping
{
    public class EmployeeProfile : Profile
    {
         public   EmployeeProfile() 
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();

        }
    }
}
