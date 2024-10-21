using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicesCheck.Models
{
    public class ServiceNames
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public char Type { get; set; }
        public bool IsActive { get; set; }
    }
}
