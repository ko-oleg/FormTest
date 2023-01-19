using Microsoft.EntityFrameworkCore;

namespace FormTest.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}