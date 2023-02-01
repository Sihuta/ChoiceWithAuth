using System.Diagnostics.CodeAnalysis;

namespace ChoiceWithAuth.ViewModels
{
    public class Discipline
    {
        public int DisciplineId { set; get; }
        public int TeacherId { set; get; }
        [AllowNull]
        public string Title { set; get; }
        //
        public Teacher? Teacher { set; get; }
        public List<Student>? Students { set; get; }
    }
}
