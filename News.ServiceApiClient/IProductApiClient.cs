﻿using News.ViewModel.Catalog.Image;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetAllProduct(ProductPagingRequest request);
        Task<ProductViewModel> GetProductById(int id);
        Task<ApiResult<bool>> CreateProduct(CreateProductRequestModel pro);
        Task<ApiResult<bool>> UpdateProduct(int id, UpdateProductRequestModel pro);
        Task<ApiResult<bool>> DeleteProduct(DeleteProductRequestModel pro);

        Task<List<ImageVM>> GetListImages(int productId);
        Task<ImageVM> GetImageById(int id);
        Task<ApiResult<bool>> AddImage(AddImageRequest request);
        Task<ApiResult<bool>> DeleteImage(DeleteImageRequest request);
        Task<ApiResult<bool>> SetImage(int productId, SetDefaultImageRequest request);
    }
}
