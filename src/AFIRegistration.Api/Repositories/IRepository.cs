using AFIRegistration.Api.Entities;
using System.Threading.Tasks;

namespace AFIRegistration.Api.Services
{
    public interface IRepository<Entity> where Entity:BaseEntity
    {
        Task<Entity> GetByIdAsync(int id);
        Task AddItemAsync(Entity item);
        Task<bool> SaveAsync();
    }
}
