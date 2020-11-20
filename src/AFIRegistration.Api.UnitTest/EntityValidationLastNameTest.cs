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
    class EntityValidationLastNameTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_Customer_LastName_Required()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .Without(x => x.LastName)
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.LastNameRequired, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_LastName_LessThan3Char()
        {
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "jj")
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.LastNameMinLength, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_LastName_Equal3Char()
        {
            // Arr
            var fixture = new Fixture();

            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.FirstName, "Wheer")
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
        public void Validate_Customer_LastName_GreaterThan3Char()
        {
            // Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "Abcde")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
        }
        public void Validate_Customer_LastName_GreaterThan50Char()
        {
            // Arr
            var fixture = new Fixture();
            var contrainedLastName = TestHelper.GetrandomString(55);
            var customer = fixture.Build<Customer>()
                .With(x => x.FirstName, "abcd")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.LastName, contrainedLastName)
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
