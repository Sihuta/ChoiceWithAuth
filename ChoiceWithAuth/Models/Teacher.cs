using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChoiceWithAuth.ViewModels
{
    public class Teacher : IValidatableObject
    {
        public int TeacherId { set; get; }
        [AllowNull]
        public string Name { set; get; }
        public string Dept { set; get; } = "";
        //
        public List<Discipline>? Disciplines { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool IsForbidden(string prop)
            {
                string[] forbiddens = { "aaa", "bbb", "ccc" };
                return forbiddens.Any(f => prop == f);
            }
            if (IsForbidden(Name))
                yield return new ValidationResult("Name is a forbidden word.", new string[] { "Name" });
            if (IsForbidden(Dept))
                yield return new ValidationResult("Dept is a forbidden word.", new string[] { "Dept" });
        }
    }
}
