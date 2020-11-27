using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//code first approach

namespace ShoppingCart.Domain.Models
{
    public class Product
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public virtual Category Category { get; set; } //this is the relationship

        [ForeignKey("Category")]
        public int CategoryId { get; set; } //this is the actual fk; this is a way how to addess the relationship

        public string ImageUrl { get; set; }


        public int Stock { get; set; }
    }
}
