using Microsoft.EntityFrameworkCore;
using GsvPersistence.DataModels;

namespace GsvPersistence
{
    /// <summary>
    /// The GSV Database context.
    /// </summary>
    class GsvDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextOptions">Database context options.</param>
        public GsvDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            if (Database.EnsureCreated())
            {
                // new database created
            }
        }

        /// <summary>
        /// The template DbSet
        /// </summary>
        public DbSet<TemplateDataModel> Templates { get; set; }

        /// <summary>
        /// The client DbSet
        /// </summary>
        public DbSet<ClientDataModel> Clients { get; set; }

        /// <summary>
        /// Builds all data models for the database.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<ClientDataModel>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<ClientDataModel>()
                .Property(t => t.Name);
            modelBuilder.Entity<ClientDataModel>()
                .Property(t => t.TemplateOptions);

            modelBuilder.Entity<TemplateDataModel>()
               .HasKey(t => t.Id);
            modelBuilder.Entity<TemplateDataModel>()
                .Property(t => t.Name);
            modelBuilder.Entity<TemplateDataModel>()
                .Property(t => t.ClientId);
            modelBuilder.Entity<TemplateDataModel>()
                .Property(t => t.Data);
        }
    }
}
