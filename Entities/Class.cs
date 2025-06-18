namespace SchoolWebApplication.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassTeacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
