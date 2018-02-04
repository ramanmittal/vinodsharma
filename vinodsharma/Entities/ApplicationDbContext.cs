using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vinodsharma.Models;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

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
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Members).WithRequired(x => x.User).HasForeignKey(x => x.UserId);
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
            if (Database.CurrentTransaction == null)
            {
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
            else
            {
                var log = "";
                try
                {
                    Database.Log = x => log = log + x;
                    base.SaveChanges();
                }
                catch (Exception ex)
                {                    
                    throw new Exception(string.Format("Error occurred during running the script {0}", log), ex);
                }
            }
            
        }

        private string GetTableName<T>() where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            return GetTableName<T>(objectContext);
        }

        private string GetTableName<T>(ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);
            string table = match.Groups["table"].Value;
            return table;
        }
        public void ApplyLock<T>() where T : class
        {
            Database.ExecuteSqlCommand("SELECT TOP 0 NULL FROM " + GetTableName<T>() + " WITH (TABLOCKX)");
        }

        public DbSet<Member> Members { get; set; }
    }
}