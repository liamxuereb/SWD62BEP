using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public int Qty { get; set; }

        public virtual ProductViewModel Product { get; set; }

        public Guid ProductID { get; set; }

        public int CartID { get; set; }
    }
}
