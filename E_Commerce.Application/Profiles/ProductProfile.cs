using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dst=>dst.ProductBrand,opt=>opt.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dst=>dst.ProductType,opt=>opt.MapFrom(src=>src.ProductType.Name));
        }
    }
}
