using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Position)
                .WithMany(p => p.Teachers)
                .HasForeignKey(t => t.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Student)
                .WithMany()
                .HasForeignKey(j => j.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Class)
                .WithMany()
                .HasForeignKey(j => j.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Subject)
                .WithMany()
                .HasForeignKey(j => j.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Teacher)
                .WithMany()
                .HasForeignKey(j => j.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Teacher)
                .WithMany()
                .HasForeignKey(u => u.TeacherId)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithMany()
                .HasForeignKey(u => u.StudentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Директор" },
                new Position { Id = 2, Name = "Заступник з НВР" },
                new Position { Id = 3, Name = "Заступник з ВР" },
                new Position { Id = 4, Name = "Вчитель молодших класів" },
                new Position { Id = 5, Name = "Вчитель середніх і старших класів" }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "8", ClassTeacher = "Сташко Ольга Іванівна" },
                new Class { Id = 2, Name = "9", ClassTeacher = "Змійовська Валентина Миколаївна" },
                new Class { Id = 3, Name = "10", ClassTeacher = "Потічко Оксана Анатоліївна" },
                new Class { Id = 4, Name = "11", ClassTeacher = "Бадюк Марина Тодосіївна" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, LastName = "Бадюк", FirstName = "Марина", MiddleName = "Тодосіївна", PositionId = 5, Experience = 32 },
                new Teacher { Id = 2, LastName = "Потічко", FirstName = "Оксана", MiddleName = "Анатоліївна", PositionId = 5, Experience = 38 },
                new Teacher { Id = 3, LastName = "Сташко", FirstName = "Ольга", MiddleName = "Іванівна", PositionId = 5, Experience = 25 },
                new Teacher { Id = 4, LastName = "Змійовська", FirstName = "Валентина", MiddleName = "Миколаївна", PositionId = 5, Experience = 14 },
                new Teacher { Id = 5, LastName = "Незнайко", FirstName = "Мар'яна", MiddleName = "Семенівна", PositionId = 1, Experience = 18 },
                new Teacher { Id = 6, LastName = "Манкиш", FirstName = "Михайло", MiddleName = "Васильович", PositionId = 4, Experience = 17 },
                new Teacher { Id = 7, LastName = "Колесник", FirstName = "Галина", MiddleName = "Михайлівна", PositionId = 3, Experience = 24 },
                new Teacher { Id = 8, LastName = "Казимір", FirstName = "Галина", MiddleName = "Петрівна", PositionId = 2, Experience = 36 }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Математика", TeacherId = 5 },
                new Subject { Id = 2, Name = "Українська мова", TeacherId = 1 },
                new Subject { Id = 3, Name = "Географія", TeacherId = 4 },
                new Subject { Id = 4, Name = "Історія України", TeacherId = 2 },
                new Subject { Id = 5, Name = "Фізична культура", TeacherId = 6 },
                new Subject { Id = 6, Name = "Біологія", TeacherId = 3 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, LastName = "Чорней", FirstName = "Катерина", MiddleName = "Михайлівна", ClassId = 4 },
                new Student { Id = 2, LastName = "Курчак", FirstName = "Ольга", MiddleName = "Дмитрівна", ClassId = 4 },
                new Student { Id = 3, LastName = "Прокопець", FirstName = "Михайло", MiddleName = "Дмитрович", ClassId = 1 },
                new Student { Id = 4, LastName = "Пітік", FirstName = "Юлія", MiddleName = "Андріївна", ClassId = 3 },
                new Student { Id = 5, LastName = "Жалоба", FirstName = "Олександра", MiddleName = "Михайлівна", ClassId = 2 },
                new Student { Id = 6, LastName = "Лунгул", FirstName = "Богдан", MiddleName = "Сергійович", ClassId = 3 }
            );

            modelBuilder.Entity<Journal>().HasData(
                new Journal
                {
                    Id = 1,
                    Date = DateTime.SpecifyKind(new DateTime(2025, 9, 1), DateTimeKind.Utc),
                    Semester = "I",
                    StudentId = 1,
                    ClassId = 4,
                    SubjectId = 2,
                    TeacherId = 1,
                    Mark = 12
                }
            );
        }
    }
}