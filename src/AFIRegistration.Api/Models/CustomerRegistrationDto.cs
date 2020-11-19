using System;

namespace AFIRegistration.Api.Models
{
    public class CustomerRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyReferenceNumber { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
