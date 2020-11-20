using AFIRegistration.Api.Entities;
using AFIRegistration.Api.Helpers;
using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AFIRegistration.Api.UnitTest
{
    public class EntityValidationEmailAndDOBTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_Customer_DOB_or_Email_Required()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .Without(x => x.Email)
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .Without(x => x.DateOfBirth)
                .Create();
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.DateOfBirthOrEmailRequired, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_DOB_LessThan18_Failed()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .Without(x => x.Email)
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.Today.AddYears(-17))
                .Create();
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.DateOfBirthMinAgeConstrain, validationResults[0].ErrorMessage);

        }
        [Test]
        public void Validate_Customer_DOB_GreaterThan18_Pass()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .Without(x => x.Email)
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.Today.AddYears(-19))
                .Create();
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
        }
        [Test]
        public void Validate_Customer_Email_Format()
        {
            // Arr
            var fixture = new Fixture();
            var pattern = @"[a-zA-Z0-9]{4,50}@[a-zA-Z0-9]{2,50}.(com|co.uk)";
            var email =
                new SpecimenContext(fixture).Resolve(
                    new RegularExpressionRequest(pattern));
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .With(x => x.Email, email)
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, "AA-666666")
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
            Assert.True(Regex.IsMatch(customer.Email, pattern));

        }
    }
}
