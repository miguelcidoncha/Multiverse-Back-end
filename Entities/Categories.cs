﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Categories
    {
        [Key]
        public int IdCategories { get; set; }
        public string CategoriesName { get; set; }


    }
}
