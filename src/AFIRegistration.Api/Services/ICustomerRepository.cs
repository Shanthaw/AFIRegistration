using AFIRegistration.Api.Entities;

namespace AFIRegistration.Api.Services
{
    public interface ICustomerRepository<Entity>:IRepository<Entity> where Entity:BaseEntity
    {
    }
}
