using AFIRegistration.Api.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace AFIRegistration.Api.Entities
{
    public class Customer : BaseEntity, IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FirstNameRequired)]
        [MinLength(3, ErrorMessage = Constants.FirstNameMinLength)]
        [MaxLength(50, ErrorMessage = Constants.FirstNameMaxLength)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = Constants.LastNameRequired)]
        [MinLength(3, ErrorMessage = Constants.LastNameMinLength)]
        [MaxLength(50, ErrorMessage = Constants.LastNameMaxLength)]
        public string LastName { get; set; }
        [Required(ErrorMessage = Constants.PolicyNumberRequired)]
        public string PolicyReferenceNumber { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string policyReferenceNumberPattern = @"[A_Z]{2}-[0-9]{6}";
            string emailPattern = @"[a-zA-Z0-9]{4,50}@[a-zA-Z0-9]{2,50}.(com|co.uk)";
            if (DateOfBirth == DateTime.MinValue && string.IsNullOrEmpty(Email))
            {
                yield return new ValidationResult(Constants.DateOfBirthOrEmailRequired, new string[] { nameof(DateOfBirth), nameof(Email) });
            }
            if (DateOfBirth != null && DateOfBirth.GetCurrentAge() < 18)
            {
                yield return new ValidationResult(Constants.DateOfBirthMinAgeConstrain, new string[] { nameof(DateOfBirth) });
            }
            var policyReferenceNumberMatched = Regex.Match(PolicyReferenceNumber, policyReferenceNumberPattern);
            if (!policyReferenceNumberMatched.Success)
            {
                yield return new ValidationResult(Constants.PolicyReferenceNumberFormat,
                    new string[] { nameof(PolicyReferenceNumber) });
            }

            if (!string.IsNullOrEmpty(Email))
            {
                var emailMatched = Regex.Match(Email, emailPattern);
                if (!emailMatched.Success)
                {
                    yield return new ValidationResult(Constants.EmailFormat,
                        new string[] { nameof(Email) });
                }
            }
        }
    }
}
