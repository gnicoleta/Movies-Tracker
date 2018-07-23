using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace WebApplication.DatabaseModels
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Series> Series { get; set; }

        public DatabaseContext() : 
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = @"D:\WebApplicationvProiect\WebApplication\Database\sqlite.db",
                    ForeignKeys = true,
                    BinaryGUID = false
                }.ConnectionString
            }, true)
        {
            Database.SetInitializer<DatabaseContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}