using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class CreateStudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public int StudentRollNo { get; set; }

        public string StudentName { get; set; }

        public string StudentPhone { get; set; }

        public string StudentEmail { get; set; }

        public int ClassId { get; set; }
    }
}
