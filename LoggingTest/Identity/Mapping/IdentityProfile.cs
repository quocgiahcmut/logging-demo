using AutoMapper;
using LoggingTest.Identity.Models;
using LoggingTest.Identity.Resources;

namespace LoggingTest.Identity.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<RegistrationResource, ApiUser>();
            CreateMap<ApiUser, EmployeeResource>();
        }
    }
}
