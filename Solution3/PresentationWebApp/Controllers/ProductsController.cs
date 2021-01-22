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
using X.PagedList;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService,
             IWebHostEnvironment env )
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            var allCategories = _categoriesService.GetCategories();
            ViewBag.Categories = allCategories;

            var list = _productsService.GetProducts();
            ViewBag.ListPages = GetPages(page, list);
            ViewBag.Search = false;

            return View(GetPages(page, list));
        }

        [HttpPost]
        public IActionResult Search(int category, int pg=1) //using a form, and the select list must have name attribute = category
        {
            //var list = _productsService.GetProducts(category).ToList();

            //return View("Index", list);

            var categeories = _categoriesService.GetCategories();
            ViewBag.Categories = categeories;
            var list = _productsService.GetProducts(category, false);
            ViewBag.ListPages = GetPages(pg, list);

            //Pagination handler for active page
            ViewBag.Search = true;

            return View("Index", list);
        }


        public IActionResult Hide(Guid id)
        {
            try
            {
                _productsService.HideProduct(id);
                TempData["feedback"] = "Hide Button Pressed!";
            }
            catch(Exception ex)
            {
                TempData["Warning"] = "Hide Action Unsuccessful!";
            }

            return RedirectToAction("Index");
        }


        public IActionResult Details(Guid id)
        {
            var p = _productsService.GetProduct(id);
            return View(p);
        }

        //the engine will load a page with empty fields
        [HttpGet]
        [Authorize (Roles ="Admin")] //is going to be accessed only by authenticated users
        public IActionResult Create()
        {
            //fetch a list of categories
            var listOfCategeories = _categoriesService.GetCategories();

            //we pass the categories to the page
            ViewBag.Categories = listOfCategeories;

            return View();
        }

        //here details input by the user will be received
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f)
        {
            try
            {
                if(f !=  null)
                {
                    if(f.Length > 0)
                    {
                        //C:\Users\Ryan\source\repos\SWD62BEP\SWD62BEP\Solution3\PresentationWebApp\wwwroot
                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFilenameWithAbsolutePath = _env.WebRootPath +  @"\Images\" + newFilename;
                        
                        using (var stream = System.IO.File.Create(newFilenameWithAbsolutePath))
                        {
                            f.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + newFilename;
                    }
                }

                _productsService.AddProduct(data);

                TempData["feedback"] = "Product was added successfully";
            }
            catch (Exception ex)
            {
                //log error
                TempData["warning"] = "Product was not added!";
            }

           var listOfCategeories = _categoriesService.GetCategories();
           ViewBag.Categories = listOfCategeories;
            return View(data);
        
        } //fiddler, burp, zap, postman

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                //log your error 

                TempData["warning"] = "Product was not deleted"; //Change from ViewData to TempData
            }

            return RedirectToAction("Index");
        }

        protected IPagedList<ProductViewModel> GetPages(int? page, IQueryable<ProductViewModel> products)
        {
            if (page.HasValue && page < 1)
                return null;

            var list = products;

            // paginating the list
            const int size = 10;
            var pages = list.ToPagedList(page ?? 1, size);

            if (pages.PageNumber != 1 && page.HasValue && page > pages.PageCount)
                return null;

            return pages;
        }
    }
}
