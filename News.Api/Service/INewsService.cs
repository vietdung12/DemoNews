﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;

namespace News.Api.Service
{
    public interface INewsService
    {
        Task<PagedResult<ProductViewModel>> GetAllProduct(ProductPagingRequest request);
        Task<ProductViewModel> GetProductById(int id);
        Task<int> CreateProduct(CreateProductRequestModel requestModel);
        Task<ApiResult<bool>> UpdateProduct(int id, UpdateProductRequestModel pro);
        Task<ApiResult<bool>> DeleteProduct(int id);
    }
}
