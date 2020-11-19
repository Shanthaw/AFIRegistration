using System;
using System.Threading.Tasks;
using AFIRegistration.Api.Entities;
using AFIRegistration.Api.Models;
using AFIRegistration.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AFIRegistration.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository<Customer> customerRepository,
            IMapper mapper, ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerRegistrationDto registrationDto)
        {
            var customerEntity = _mapper.Map<Customer>(registrationDto);
            await _customerRepository.AddItemAsync(customerEntity)
                .ConfigureAwait(false);
            await _customerRepository.SaveAsync()
                .ConfigureAwait(false);
            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            _logger.LogInformation($"a customer id {customerToReturn.Id} is registered");
            return CreatedAtRoute("GetCustomer", new { id = customerToReturn.Id }, customerToReturn);
        }
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetRegisteredCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id)
                .ConfigureAwait(false);
            if (customer == null)
                return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
    }
}
