using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Data.Entities;
using News.ViewModel.Common;

namespace News.Api.Service
{
    public interface INewsService
    {        
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(int id);
        Task<ApiResult<bool>> CreateProduct(Product pro);
        Task<ApiResult<bool>> UpdateProduct(Product pro);
        Task<ApiResult<bool>> DeleteProduct(Product pro);
    }
}
