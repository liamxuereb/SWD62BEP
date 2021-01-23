using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public virtual Cart Cart { get; set; }

        [ForeignKey("Cart")]
        public int CartID { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }

    }
}
