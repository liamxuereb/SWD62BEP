using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IProductsService
    {
        IQueryable<ProductViewModel> GetProducts();

        //IQueryable<ProductViewModel> GetProducts(string keyword);

        IQueryable<ProductViewModel> GetProducts(int category);

        IQueryable<ProductViewModel> GetProducts(int category, bool disabled);

        ProductViewModel GetProduct(Guid id);

        void AddProduct(ProductViewModel product);

        void DeleteProduct(Guid id);

        void HideProduct(Guid id);
        
    }
}
