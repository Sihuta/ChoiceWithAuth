using ChoiceWithAuth.Attributes;
using ChoiceWithAuth.ViewModels;

namespace ChoiceWithAuth.ViewModels
{
    public class SelectViewModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public List<Discipline>? Disciplines { get; set; }

        [LimitSelectedDisciplines(Constants.DisciplinesMaxNumber)]
        public int[] SelectedDiscIds { get; set; } = Array.Empty<int>();
    }
}
