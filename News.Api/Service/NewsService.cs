using Microsoft.EntityFrameworkCore;
using News.Data.EF;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext _context;

        public NewsService(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateProduct(Product pro)
        {
           if(pro == null)
           {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
           }
             _context.Products.Add(pro);
             await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteProduct(Product pro)
        {
            if(pro == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
             _context.Products.Remove(pro);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var Item = await _context.Products.ToListAsync();
            return  Item;
        }

        public async Task<Product> GetProductById(int id)
        {
            var Item = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return Item;
        }       

        public async Task<ApiResult<bool>> UpdateProduct(Product pro)
        {
            if (pro == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
