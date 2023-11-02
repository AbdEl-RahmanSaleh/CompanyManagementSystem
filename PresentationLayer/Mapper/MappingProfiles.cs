using AutoMapper;
using DataAccessLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel ,Employee>().ReverseMap();
            CreateMap<DepartmentViewModel ,Department>().ReverseMap();
        }
    }
}
