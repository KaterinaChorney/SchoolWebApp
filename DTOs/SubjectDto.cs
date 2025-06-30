namespace SchoolWebApplication.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? TeacherFullName { get; set; }
    }
}