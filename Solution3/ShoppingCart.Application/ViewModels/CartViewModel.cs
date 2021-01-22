using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    { 
        public int Id { get; set; }

        public ProductViewModel product { get; set; }

        public string Email { get; set; }

        public int Qty { get; set; }
    }
}
