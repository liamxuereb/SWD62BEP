using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;


        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, IWebHostEnvironment env)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;
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
        [Authorize (Roles ="Admin")] //is going to be access by authenticated users
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f)
        {
            try
            {
                if(f != null)
                {
                    if (f.Length > 0)
                    {
                        //this will go in db
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        //this to save the actual file
                        string newFileNameWithAbsolutePath =_env.WebRootPath + @"\Images\" + newFileName;

                        using (var stream = System.IO.File.Create(newFileNameWithAbsolutePath))
                        {
                            f.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + newFileName;

                    }
                }
                


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

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch(Exception ex)
            {
                TempData["Warning"] = "Product was not deleted!";  //to do: change from ViewData to TempData in order to see message after redirection
            }
            return RedirectToAction("Index");
        }

    }
}
