namespace SchoolWebApplication.Entities
{
    public class Journal
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string Semester { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int Mark { get; set; }

    }
}
