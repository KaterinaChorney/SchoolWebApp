using System.Security.Claims;

namespace SchoolWebApplication.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }     
        public string FirstName { get; set; }    
        public string MiddleName { get; set; }    

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}