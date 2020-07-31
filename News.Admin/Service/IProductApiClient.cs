using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Admin.Service
{
    public interface IProductApiClient
    {
        Task<IEnumerable<ProductViewModel>> GetAllProduct();
        Task<ProductViewModel> GetProductById(int id);
        Task<ApiResult<bool>> CreateProduct(CreateProductRequestModel pro);
        Task<ApiResult<bool>> UpdateProduct(int id, UpdateProductRequestModel pro);
        Task<ApiResult<bool>> DeleteProduct(int id);
    }
}
