namespace SchoolWebApplication.DTOs
{
    public class CreateJournalDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public DateTime Date { get; set; }
        public string Semester { get; set; } = null!;
        public int Mark { get; set; }
    }
}