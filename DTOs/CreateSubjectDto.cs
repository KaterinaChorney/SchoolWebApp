namespace SchoolWebApplication.DTOs
{
    public class CreateSubjectDto
    {
        public string Name { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}