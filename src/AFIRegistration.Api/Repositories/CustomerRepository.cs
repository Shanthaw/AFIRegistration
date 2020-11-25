using AFIRegistration.Api.Contexts;
using AFIRegistration.Api.Entities;
using AFIRegistration.Api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AFIRegistration.Api.Services
{
    public class CustomerRepository : ICustomerRepository<Customer>, IDisposable
    {
        private readonly CustomerContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(CustomerContext context
            ,ILogger<CustomerRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task AddItemAsync(Customer item)
        {
            await _context.AddAsync(item).ConfigureAwait(false);
        }
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<bool> SaveAsync()
        {
            var validationErrors = _context.ChangeTracker
           .Entries<IValidatableObject>()
           .SelectMany(e =>
           {
               var validationResults = new List<ValidationResult>();
               Validator.TryValidateObject(e.Entity, new ValidationContext(e.Entity), validationResults, true);
               return validationResults;
           }).Where(r => r != ValidationResult.Success);

            if (validationErrors.Any())
            {
                _logger.LogError($"validation errors {string.Join("\r\n ", validationErrors)}");
                throw new EntityException(string.Join("\r\n ", validationErrors));
            }
            return ((await _context.SaveChangesAsync().ConfigureAwait(false)) >= 0);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
