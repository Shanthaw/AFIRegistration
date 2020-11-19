using AutoMapper;

namespace AFIRegistration.Api.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Models.CustomerRegistrationDto, Entities.Customer>();
            CreateMap<Entities.Customer, Models.CustomerRegistrationDto>();
            CreateMap<Entities.Customer, Models.CustomerDto>();
        }
    }
}
