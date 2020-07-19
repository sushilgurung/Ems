using Library.DataAcessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Context
{
    public class DatabaseEntities : DbContext
    {
        public DatabaseEntities() : base("name=DataBaseConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //  Database.SetInitializer(new DataInitializer());
        }
        //public DatabaseEntities(DbContextOptions<DatabaseEntities> options) : base(options)
        //{

        //}
        //public DatabaseEntities(string connectionString) : base(connectionString)
        //{
        //    this.Configuration.LazyLoadingEnabled = false;
        //}

        public virtual DbSet<SuspendedIP> SuspendedIP { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserInRoles> UserInRoles { get; set; }
        public virtual DbSet<Roles> Role { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<LoginActivities> LoginActivities { get; set; }


        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Configurations.Add(new CourseConfiguration());
        }
    }
}
