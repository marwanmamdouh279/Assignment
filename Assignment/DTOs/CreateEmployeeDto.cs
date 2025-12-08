using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage ="Name Is Required!!")]
        public string Name { get; set; }
        [Range(20,40,ErrorMessage =" Age Must be between 20 and 40 ")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Please Enter Valid Email!")]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
    }
}
