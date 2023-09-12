using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
