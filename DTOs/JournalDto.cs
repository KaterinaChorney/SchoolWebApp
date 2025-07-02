namespace SchoolWebApplication.DTOs
{
    public class JournalDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Semester { get; set; } = null!;
        public int Mark { get; set; }
    }
}