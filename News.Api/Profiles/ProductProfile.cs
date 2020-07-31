﻿using AutoMapper;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<CreateProductRequestModel, Product>();
            CreateMap<UpdateProductRequestModel, Product>();
        }
    }
}