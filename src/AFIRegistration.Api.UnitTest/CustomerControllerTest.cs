using AFIRegistration.Api.Controllers;
using AFIRegistration.Api.Entities;
using AFIRegistration.Api.Models;
using AFIRegistration.Api.Services;
using AFIRegistration.Api.Utils;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AFIRegistration.Api.UnitTest
{
    public class CustomerControllerTest
    {
        private Mock<ICustomerRepository<Customer>> _customerRepo;
        private Mock<IMapper> _mapper;
        private Mock<ILogger<CustomersController>> _logger;

        [SetUp]
        public void Setup()
        {
            _customerRepo = new Mock<ICustomerRepository<Customer>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CustomersController>>();
        }

        [Test]
        public async Task RegisterCustomer_Success()
        {
            //Arr
            var customer = new Customer();
            var fixture = new Fixture();
            var customerDto = fixture.Create<CustomerRegistrationDto>();
            customer.DateOfBirth = customerDto.DateOfBirth;
            customer.Email = customerDto.Email;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.PolicyReferenceNumber = customerDto.PolicyReferenceNumber;

            var customerDToReturn = new CustomerDto();

            _customerRepo.Setup(a => a.AddItemAsync(customer)).Returns(Task.CompletedTask);
            _customerRepo.Setup(a => a.SaveAsync()).Callback(()
                => { customer.Id = 100; customerDToReturn.Id = customer.Id; })
                .ReturnsAsync(true);
            _mapper.Setup(a => a.Map<Customer>(customerDto)).Returns(customer);

            _mapper.Setup(a => a.Map<CustomerDto>(customer)).Returns(customerDToReturn);
            var controller = new CustomersController(_customerRepo.Object, _mapper.Object, _logger.Object);

            //Act
            var result = await controller.RegisterCustomer(customerDto);

            //Ass
            _customerRepo.Verify(a => a.AddItemAsync(It.IsAny<Customer>()), Times.Exactly(1));
            _customerRepo.Verify(a => a.SaveAsync(), Times.Exactly(1));
            Assert.AreEqual(StatusCodes.Status201Created, (result as CreatedAtRouteResult).StatusCode);
            Assert.AreEqual(customer.Id, ((Envelope<CustomerDto>)(result as CreatedAtRouteResult).Value).Result.Id);
        }

        [Test]
        public async Task GetRegistredCustomerById_Success()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
            };
            _customerRepo.Setup(a => a.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mapper.Setup(a => a.Map<CustomerDto>(customer)).Returns(customerDto);
            var controller = new CustomersController(_customerRepo.Object, _mapper.Object, _logger.Object);

            //Act
            var result = await controller.GetRegisteredCustomer(customer.Id);

            //Ass
            _customerRepo.Verify(a => a.GetByIdAsync(customer.Id), Times.Exactly(1));
            Assert.AreEqual(StatusCodes.Status200OK, (result as OkObjectResult).StatusCode);
            Assert.AreEqual(customer.Id, ((Envelope<CustomerDto>)(result as OkObjectResult).Value).Result.Id);
        }

        [Test]
        public async Task GetRegistredCustomerById_NotFound()
        {
            //Arr
            int customerId = 100;
            _customerRepo.Setup(a => a.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);
            var controller = new CustomersController(_customerRepo.Object, _mapper.Object, _logger.Object);

            //Act
            var result = await controller.GetRegisteredCustomer(customerId);

            //Ass
            _customerRepo.Verify(a => a.GetByIdAsync(customerId), Times.Exactly(1));
            Assert.AreEqual(StatusCodes.Status404NotFound, (result as NotFoundResult).StatusCode);
        }
    }
}
