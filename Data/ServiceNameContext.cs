using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServicesCheck.Models;

namespace WindowsServicesCheck.Data
{
    public class ServiceNameContext : DbContext
    {
        public ServiceNameContext(DbContextOptions<ServiceNameContext> options) : base(options)
        {

        }

        public DbSet<ServiceNames> ServiceNames { get; set; }
    }
}
