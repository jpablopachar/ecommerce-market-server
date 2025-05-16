using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    internal class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
    }
}
