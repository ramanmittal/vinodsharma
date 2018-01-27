using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vinodsharma.Models;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace vinodsharma.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.Member).WithRequired(x => x.User).Map(x=>x.MapKey("MemberID")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Member>().HasOptional(x => x.Upliner).WithMany(x => x.Members).HasForeignKey(x => x.UplineId).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public override int SaveChanges()
        {
            return 0;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<int>(0);
        }

        public void Commit() {
            using (var transaction = Database.BeginTransaction())
            {
                var log = "";
                try
                {
                    Database.Log = x => log = log + x;
                    base.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(string.Format("Error occurred during running the script {0}", log), ex);
                }
            }
        }

        public DbSet<Member> Members { get; set; }
    }
}