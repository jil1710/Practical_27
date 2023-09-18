using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core_Practical_17.Models
{
    public class Student
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-z ]){2,20}", ErrorMessage = "Name can only have alphabets")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is not in Valid Formate!")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName="date")]
        [DataType(DataType.Date, ErrorMessage = "Only Date is Required"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
