using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

    }
}