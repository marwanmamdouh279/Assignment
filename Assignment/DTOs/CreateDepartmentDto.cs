using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Code Is Reqquired !")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Name Is Reqquired !")]

        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt Is Reqquired !")]
        public DateTime CreateAt { get; set; }
    }
}
