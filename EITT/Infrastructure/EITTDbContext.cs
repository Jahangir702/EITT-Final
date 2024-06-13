using Domain.Entities;
using Microsoft.EntityFrameworkCore;

/*
 * Created by    : Jahangir Alam
 * Date created  : 17.04.2024
 * Modified by   :
 * Last modified : 
 * Reviewed by   : 
 * Date Reviewed : 
 */
namespace Infrastructure
{
    public class EITTDbContext : DbContext
    {
        public EITTDbContext(DbContextOptions<EITTDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<IssueLog> IssueLogs { get; set; }
        public DbSet<SolveSheet> SolveSheets { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}