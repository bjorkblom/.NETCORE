using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Name { get; set; }
        
        [Required, MaxLength(255)]
        public string Location { get; set; }
        public CusisineType Cusisine { get; set; }
    }
}
