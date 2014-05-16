using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
   /* public class UserRequestContext : DbContext
    {
        public UserRequestContext()
            : base("UserRequestContext")
        {

        }
        public DbSet<UserRequest> userRequest { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }*/
}