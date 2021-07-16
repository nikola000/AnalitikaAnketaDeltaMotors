using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System.Data.Entity;
using UnitOfWorkExample.UnitOfWork.Models;

namespace UnitOfWorkExample.UnitOfWork
{
 
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<ImportData> ImportDatas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subtopic> Subtopics { get; set; }
        public DbSet<Settings> Settings { get; set; }
        // Server=localhost\SQLEXPRESS;Database=AnalitikaAnketaDB;Trusted_Connection=True;
        //public DatabaseContext() : base("AnalitikaConnection")
        public DatabaseContext() : base(AnalitikaAnketaDeltaMotors.Classes.Configuration.GetInstance().ConnectionString)
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
            //Database.SetInitializer<DatabaseContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your model configuration here            
        }
    }
}
