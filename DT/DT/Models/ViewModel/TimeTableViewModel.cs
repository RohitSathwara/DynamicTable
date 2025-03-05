namespace DT.Models
{
    public class TimeTableViewModel
    {
        public int WorkingDays { get; set; }
        public int SubjectsPerDay { get; set; }
        public int TotalHours => WorkingDays * SubjectsPerDay;

        public List<SubjectViewModel> Subjects { get; set; } = new List<SubjectViewModel>();

        public List<List<string>> GeneratedTimeTable { get; set; } = new List<List<string>>();
    }
    //public class SubjectViewModel
    //{
    //    public string Name { get; set; }
    //    public int Hours { get; set; }
    //}


}
