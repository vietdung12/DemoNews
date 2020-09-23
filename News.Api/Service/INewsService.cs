using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using News.Data.Entities;
using News.ViewModel.Catalog.Image;
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

        Task<ImageVM> GetImageById(int imageId);
        Task<List<ImageVM>> GetListImages(int productId);
        Task<int> AddImage(AddImageRequest request);
        Task<ApiResult<bool>> DeleteImage(int id);
        Task<ApiResult<bool>> SetImage(int productId, SetDefaultImageRequest request);
    }
}
