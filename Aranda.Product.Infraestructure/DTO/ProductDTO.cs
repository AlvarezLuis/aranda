using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Aranda.Product.Infraestructure.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        //[JsonIgnore]
        [Required]
        public int IdCategory { get; set; }
        public string UrlImage { get; set; }
    }
}
