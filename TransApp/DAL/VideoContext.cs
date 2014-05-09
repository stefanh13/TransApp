using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    public class VideoContext : DbContext
    {
        public VideoContext() : base("VideoContext")
        {

        }

        public DbSet<Video> videos { get; set; }
        //public DbSet<Translation> translations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}