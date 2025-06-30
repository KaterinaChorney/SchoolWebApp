namespace SchoolWebApplication.DTOs
{
    public class CreateTeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PositionId { get; set; }
        public int Experience { get; set; }
    }
}