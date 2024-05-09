using AspNetCoreWebApi_Assignment2.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi_Assignment2.Domain
{
    public class BaseEntity
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First Name must have 50 characters or less")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last Name must have 50 characters or less")]
        public string LastName { get; set; }

        [Required]
        [Range(0, 3, ErrorMessage = "Invalid gender type, Gender from 0 to 3")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Phone Number must have 10 digits")]
        public string PhoneNumber { get; set; }

        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }

        [NotMapped]
        [Range(1, int.MaxValue, ErrorMessage = "Age must be greater than 0")]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
    }
}