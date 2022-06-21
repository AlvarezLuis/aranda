using System;
using System.Collections.Generic;
using System.Text;

namespace Aranda.Product.Infraestructure.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public string UrlImage { get; set; }
    }
}
