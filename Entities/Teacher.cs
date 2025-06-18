namespace SchoolWebApplication.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public int PositionId { get; set; }
        public int Experience { get; set; }

        public Position Position { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}
