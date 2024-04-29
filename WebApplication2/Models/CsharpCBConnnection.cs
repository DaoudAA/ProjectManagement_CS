using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CsharpCBContext : DbContext
    {
        public CsharpCBContext() : base("CsDBConnection")
        {     
        }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Member> Member { get; set; }

    }
}