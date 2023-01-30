using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zpnet.Models;

namespace zpnet.Models
{
    public class zpnetContext : DbContext
    {
        public zpnetContext (DbContextOptions<zpnetContext> options)
            : base(options)
        {
        }

        public DbSet<zpnet.Models.ksiazka> Ksiazka { get; set; } = default!;
        public DbSet<zpnet.Models.przepis> Przepis { get; set; } = default!;
        public DbSet<zpnet.Models.autor> Autorzy { get; set; } = default!;

    }
}
