using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using News.Api.Service.Storage;
using News.Data.EF;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext _context;
        private readonly IStorageService _storageService;

        public NewsService(NewsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<ApiResult<bool>> CreateProduct(Product pro, IFormFile image)
        {
            if (pro == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }

            if (image != null)
            {
                pro.Images = new List<Image>()
                {
                    new Image()
                    {
                        ImagePath = await this.SaveFile(image),
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        IsDefault = true,
                    }
                };
            }

            _context.Products.Add(pro);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteProduct(Product pro)
        {
            if (pro == null)
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
            return Item;
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

        //image sevice
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
