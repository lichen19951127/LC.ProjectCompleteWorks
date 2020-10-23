using LC.ProjectCompleteWorks.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.Services
{
    public class EFDBContext:DbContext
    {
        public EFDBContext(DbContextOptions<EFDBContext> opt) :base(opt)
        { 
            
        }

        public DbSet<Users> Users { get; set; }
    }
}
