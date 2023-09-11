using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ProductItem
    {
        [Key]
        public int IdProduct { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int productStock { get; set; }
        public string ProductImageURL { get; set; }

        public int IdCategories { get; set; }
        //public Categories IdCategories { get; set; }

    }
}
