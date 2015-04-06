using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WEI.Domain.Model;

namespace WEI.SqlDataAccess
{
    public class BlogContext : DbContext
    {
        public BlogContext(string connString) : base(connString){}

        public DbSet<Article> Article { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Tag> Tag { get; set; } 
 
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        // remove the default pluralizing table name convention.
        // instead of looking for "Article" table, the default name convention will looking for "Articles" table
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
