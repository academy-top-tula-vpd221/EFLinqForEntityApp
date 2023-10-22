using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLinqForEntityApp
{
    public class Company
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;

        public Country? Country { get; set; }

        public List<Employee> Employees { get; set; } = new();
    }
}
