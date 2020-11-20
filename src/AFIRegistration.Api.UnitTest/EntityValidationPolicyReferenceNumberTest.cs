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
    public class EntityValidationPolicyReferenceNumberTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_Customer_PolicyReferenceNumber_Required()
        {
            //Arr
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.FirstName, "Wheer")
                .Without(x => x.PolicyReferenceNumber)
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual(Constants.PolicyNumberRequired, validationResults[0].ErrorMessage);
        }
        [Test]
        public void Validate_Customer_PolicyReference_Regex_generate2UpperLetter_Hyphen_SixDigits()
        {
            // Arr
            var fixture = new Fixture();
            var pattern = @"[A_Z]{2}-[0-9]{6}";
            var policyReference =
                new SpecimenContext(fixture).Resolve(
                    new RegularExpressionRequest(pattern));
            var customer = fixture.Build<Customer>()
                .With(x => x.LastName, "las")
                .With(x => x.Email, "shan@gmail.com")
                .With(x => x.FirstName, "Wheer")
                .With(x => x.PolicyReferenceNumber, policyReference)
                .With(x => x.DateOfBirth, DateTime.ParseExact("02/02/1981", "dd/MM/yyyy", null))
                .Create();

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            //Ass
            Assert.AreEqual(0, validationResults.Count);
            Assert.True(Regex.IsMatch(customer.PolicyReferenceNumber, pattern));

        }

    }
}
