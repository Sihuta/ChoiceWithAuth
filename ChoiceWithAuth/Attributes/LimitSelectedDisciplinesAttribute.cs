using System.ComponentModel.DataAnnotations;

namespace ChoiceWithAuth.Attributes
{
    public class LimitSelectedDisciplinesAttribute : ValidationAttribute
    {
        private readonly int limit;

        public LimitSelectedDisciplinesAttribute(int limit)
        {
            this.limit = limit;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int[] selectedDiscIds = (int[])value;
            if (selectedDiscIds.Length > limit)
            {
                var errMessage = $"You cannot select more than {limit} disciplines.";
                return new ValidationResult(errMessage);
            }

            return ValidationResult.Success;
        }
    }
}
