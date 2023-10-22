using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLinqForEntityApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        
        public int? CompanyId { get; set; } // внешний ключ
        public Company? Company { set; get; } // навигационное свойство

        public Position Position { get; set; } = null!;
    }
}
