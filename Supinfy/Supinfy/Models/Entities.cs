using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Supinfy.Models
{
    public class Entities : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}