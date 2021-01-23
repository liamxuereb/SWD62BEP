using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        private readonly ICartItemsService _cartItemsService;
        private IWebHostEnvironment _env;
        public CartController(IProductsService productsService, ICartsService cartsService, ICartItemsService cartItemsService,
             IWebHostEnvironment env)
        {
            _productsService = productsService;
            _cartsService = cartsService;
            _cartItemsService = cartItemsService;
            _env = env;
        }
        public IActionResult Index()
        {
            var c = cart();
            return View(c);
        }

        public CartViewModel cart()
        {
            var userEmail = User.Identity.Name;
            var crt = _cartsService.GetCart(userEmail);
            if(crt == null)
            {
                CartViewModel newCrt = new CartViewModel()
                {
                    Email = userEmail,
                    Price = 0
                };

                _cartsService.AddCart(newCrt);
                crt = _cartsService.GetCart(userEmail);
            }
            return crt;
        }

        public IActionResult Add(Guid id)
        {
            try
            {
                var c = cart();
                int cid = c.Id;
                var p = _productsService.GetProduct(id);

                if(p != null)
                {
                    var item = _cartItemsService.GetCartProduct(cid, id);
                    if(item != null)
                    {
                        _cartsService.UpdateCart(c);
                        _cartItemsService.UpdateCartItem(item);

                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        CartItemViewModel items = new CartItemViewModel();
                        items.CartID = cid;
                        items.ProductID = p.Id;
                        items.Qty = 1;
                        _cartItemsService.AddCartItem(items);
                        _cartsService.UpdateCart(c);
                        return RedirectToAction("Index", "Products");
                    }
                }
                return RedirectToAction("Index", "Products");
            }
            catch(Exception ex)
            {
                TempData["warning"] = "Not added to cart!";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
