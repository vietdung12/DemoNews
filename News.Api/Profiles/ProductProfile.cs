using AutoMapper;
using News.Data.Entities;
using News.ViewModel.Catalog.Image;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Catalog.Register;
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

            CreateMap<Register, RegisterViewModel>();
            CreateMap<CreateRegisterRequest, Register>();

            CreateMap<Image, ImageVM>();
        }
    }
}
