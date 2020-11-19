using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFIRegistration.Api.Entities
{
    public class Customer : BaseEntity, IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required()]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required()]
        public string PolicyReferenceNumber { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}
