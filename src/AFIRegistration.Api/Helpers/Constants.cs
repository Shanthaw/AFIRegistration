namespace AFIRegistration.Api.Helpers
{
    public class Constants
    {
        public const string FirstNameRequired = "First Name is required";
        public const string FirstNameMinLength = "FirstName should have minimum 3 characters";
        public const string FirstNameMaxLength = "FirstName should have maximum 50 characters";

        public const string LastNameRequired = "Last Name is required";
        public const string LastNameMinLength = "Last Name have minimum 3 characters";
        public const string LastNameMaxLength = "Last Name should have maximum 50 characters";

        public const string PolicyNumberRequired = "PolicyReferenceNumber is required";
        public const string PolicyReferenceNumberFormat = "Policy Reference number should match the following format XX-999999";

        public const string DateOfBirthOrEmailRequired = "DOB or Email is required";

        public const string DateOfBirthMinAgeConstrain = "Customer should at least be 18 years old";

        public const string EmailFormat = "Email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’";
    }
}
