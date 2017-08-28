namespace Test
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Design;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The main context class. It describes the whole system database.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        // Системные
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<UVField> UVFields { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }

    // This is used for migrations.
    // It uses connection string from the environment.
    public class DatabaseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // FIXME: use args
            var connectionString = System.Environment.GetEnvironmentVariable("DATABASE");
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }


    public class Entity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
 
    public class Field
    {
        public int Id { get; set; }
        [Required]
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class UVField
    {
        public int Id { get; set; }
        [Required]
        public int FieldId { get; set; }
        public Field Field { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
