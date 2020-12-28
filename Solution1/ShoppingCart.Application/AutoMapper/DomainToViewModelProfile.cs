using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {

        public DomainToViewModelProfile()
        {
            //informing automapper library that we are mapping Product onto ProductViewModel
            CreateMap<Product, ProductViewModel>(); //.ForMember(x->x.DestColumn, opt->opt.MapFrom(src->src.SrcColumn)); 

            CreateMap<Category, CategoryViewModel>();

            CreateMap<Member, MemberViewModel>();
        }

    }
}
