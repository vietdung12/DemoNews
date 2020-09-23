using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using News.Api.Service.Storage;
using News.Data.EF;
using News.Data.Entities;
using News.ViewModel.Catalog.Image;
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
        private readonly IMapper _mapper;

        public NewsService(NewsDbContext context, IStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<int> AddImage(AddImageRequest request)
        {
            var productImage = new Image() 
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,               
                IsDefault = false,
                ProductId = request.ProductId,
                
            };
            if(request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
            }

            _context.Images.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> CreateProduct(CreateProductRequestModel requestModel)
        {
            var Model = _mapper.Map<Product>(requestModel);           

            if (requestModel.Image != null)
            {
                Model.Images = new List<Image>()
                {
                    new Image()
                    {
                        ImagePath = await this.SaveFile(requestModel.Image),
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        IsDefault = true,
                    }
                };
            }

            _context.Products.Add(Model);
            await _context.SaveChangesAsync();
            return Model.Id;
        }

        public async Task<ApiResult<bool>> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return new ApiErrorResult<bool>("hình ảnh không tồn tại");
            }
            await _storageService.DeleteFileAsync(image.ImagePath);

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }

            var images = _context.Images.Where(i => i.ProductId == id);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllProduct(ProductPagingRequest request)
        {
            //select
            var query = from p in _context.Products
                        join i in _context.Images on p.Id equals i.ProductId into pi
                        from i in pi.DefaultIfEmpty()
                        where i == null || i.IsDefault == true
                        select new { p, i};

            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Title.Contains(request.Keyword)
                 || x.p.Local.Contains(request.Keyword)
                 || x.p.Description.Contains(request.Keyword)
                 || x.p.Id.ToString().Contains(request.Keyword)
                 || x.p.Price.Contains(request.Keyword));
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Local = x.p.Local,
                    Description = x.p.Description,
                    Price = x.p.Price,
                    DateCreated = x.p.DateCreated,
                    Status = x.p.Status,
                    Images = x.i.ImagePath
                }).ToListAsync();           

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;
        }

        public async Task<ImageVM> GetImageById(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            var viewModel = _mapper.Map<ImageVM>(image);
            return viewModel;
        }

        public async Task<List<ImageVM>> GetListImages(int productId)
        {
            var listImage = await _context.Images.Where(x => x.ProductId == productId).Take(5)
                .Select(i => new ImageVM()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId
                }).ToListAsync();
            
            return listImage;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var Item = await _context.Products.FindAsync(id);
            var productModel = _mapper.Map<ProductViewModel>(Item);           
            return productModel;
        }

        public async Task<ApiResult<bool>> SetImage(int productId, SetDefaultImageRequest request)
        {
            var listImage = await _context.Images.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (listImage == null)
            {
                return new ApiErrorResult<bool>("Sản phẩm không có hình ảnh");
            }

            foreach (var setImage in request.Images) 
            {
                var image = await _context.Images.FirstOrDefaultAsync(x => x.ProductId == productId && x.Id == int.Parse(setImage.Id));
                if(image.IsDefault == true && setImage.Selected == false) 
                {
                    image.IsDefault = false;
                }else if(image.IsDefault == false &&  setImage.Selected == true) 
                {
                    image.IsDefault = true;
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UpdateProduct(int id, UpdateProductRequestModel pro)
        {
            if (pro == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
            var Item = await _context.Products.FindAsync(id);
            _mapper.Map(pro, Item);

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
