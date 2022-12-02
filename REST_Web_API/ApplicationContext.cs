namespace REST_Web_API
{
    using Microsoft.EntityFrameworkCore;
    public class ApplicationContext : DbContext
    {
        public DbSet<Pump> Pumps { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pump>().HasData(
                    new Pump { Id = 1, Name = "SQЕ 1-35", H = 35, Q = 1 },
                    new Pump { Id = 2, Name = "SQЕ 1-50", H = 50, Q = 1 },
                    new Pump { Id = 3, Name = "SQЕ 1-65", H = 65, Q = 1 }
            );
        }
    }
}
