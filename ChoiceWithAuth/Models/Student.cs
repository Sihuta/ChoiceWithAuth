using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChoiceWithAuth.ViewModels
{
    public class Student
    {
        public int StudentId { set; get; }

        [Required]
        //[Remote("ValidateName", "Students")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { set; get; } = string.Empty;
        //
        public List<Discipline>? Disciplines { set; get; }
    }
}
