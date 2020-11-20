using AFIRegistration.Api.Entities;
using AFIRegistration.Api.Helpers;
using AFIRegistration.Api.UnitTest.Helpers;
using AutoFixture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AFIRegistration.Api.UnitTest
{
    public class EntityValidationFirstNameTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_Customer_FirstName_Required()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .Without(x => x.FirstName)
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.FirstNameRequired, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_FirstName_LessThan3Char()
        {
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.FirstName, "jj")
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.FirstNameMinLength, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_FirstName_Equal3Char()
        {
            // Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.FirstName, "jjj")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.LastName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
        }
        [Test]
        public void Validate_Customer_FirstName_GreaterThan3Char()
        {
            // Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.FirstName, "Abcde")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.LastName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
        }
        [Test]
        public void Validate_Customer_FirstName_GreaterThan50Char()
        {
            // Arr
            var fixture = new Fixture();
            var contrainedFirstName = TestHelper.GetrandomString(55);
            var customer = fixture.Build<Customer>()
                .With(x => x.FirstName, contrainedFirstName)
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.LastName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.FirstNameMaxLength, validationResults[0].ErrorMessage);
        }
    }
}