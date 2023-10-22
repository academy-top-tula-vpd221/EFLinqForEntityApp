using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLinqForEntityApp
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public City? Capital { get; set; }

        public List<Company> Companies { get; set; } = new();
    }
}
