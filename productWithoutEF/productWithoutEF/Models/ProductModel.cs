using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace productWithoutEF.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Product Color")]
        public string Color { get; set; }

        [DisplayName("Product Category")]
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CratedDate { get; set; }

    }
}
