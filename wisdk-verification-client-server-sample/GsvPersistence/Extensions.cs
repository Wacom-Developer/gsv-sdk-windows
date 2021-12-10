using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GsvPersistence.Interfaces; 

namespace GsvPersistence
{
    /// <summary>
    /// The Persistence layer extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Registers GSV Persistence with SQLite.
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="connectionString">The database connection string.</param>
        public static void RegisterSqlitePersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GsvDbContext>(options => options.UseSqlite(connectionString));
            services.AddTransient<IGsvRepository, GsvRepository>();
        }

        /// <summary>
        /// Registers GSV Persistence with MySQL.
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="connectionString">The database connection string.</param>
        public static void RegisterMySqlPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GsvDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            services.AddTransient<IGsvRepository, GsvRepository>();
        }
    }
}
