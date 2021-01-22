using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartsRepository
    {
        ShoppingCartDbContext _context;
        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
    }
}
