using AFIRegistration.Api.Contexts;
using AFIRegistration.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            return ((await _context.SaveChangesAsync().ConfigureAwait(false)) >= 0);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
