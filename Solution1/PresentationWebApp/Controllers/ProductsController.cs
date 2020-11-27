using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;


        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var list = _productsService.GetProducts();
            return View(list);
        }


        public IActionResult Details(Guid id)
        {
            var p = _productsService.GetProduct(id);
            return View(p);
        }

        //the engine will load a page with empty fields
        [HttpGet]
        public IActionResult Create()
        {
            //return page with empty fields

            //fetch liss of categories
            var listOfCategories = _categoriesService.GetCategories();


            //we pass the categories to the page
            ViewBag.Categories = listOfCategories;


            return View();
        }

        //here details input by the user will be recieved
        [HttpPost]
        public IActionResult Create(ProductViewModel data)
        {
            try
            {
                _productsService.AddProduct(data);
                ViewData["feedback"] = "Product was added Successfully";
            }
            catch(Exception ex)
            {
                ViewData["feedback"] = "Product was not added!!";
            }

            var listOfCategories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategories;


            return View(data);

        }

    }
}
