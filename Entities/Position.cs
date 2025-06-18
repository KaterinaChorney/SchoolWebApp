namespace SchoolWebApplication.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}
