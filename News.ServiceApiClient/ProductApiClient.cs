using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using News.ViewModel.Catalog.Image;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {

        }

        public async Task<ApiResult<bool>> AddImage(AddImageRequest request)
        {
            var requestContent = new MultipartFormDataContent();
            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "imageFile", request.ImageFile.FileName);
            }
            requestContent.Add(new StringContent(request.ProductId.ToString()), "productId");
            requestContent.Add(new StringContent(request.Caption.ToString()), "caption");           

            var dataUrl = await PostAsync<bool>($"/api/News/image", requestContent);
            return dataUrl;
        }

        public async Task<ApiResult<bool>> CreateProduct(CreateProductRequestModel pro)
        {      
            var requestContent = new MultipartFormDataContent();

            if (pro.Image != null)
            {
                byte[] data;
                using (var br = new BinaryReader(pro.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)pro.Image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "image", pro.Image.FileName);
            }

            requestContent.Add(new StringContent(pro.Title.ToString()), "title");
            requestContent.Add(new StringContent(pro.Local.ToString()), "local");
            requestContent.Add(new StringContent(pro.Description.ToString()), "description");
            requestContent.Add(new StringContent(pro.Price.ToString()), "price");
            requestContent.Add(new StringContent(pro.DateCreated.ToString()), "dateCreated");

            var dataUrl = await PostAsync<bool>($"/api/News", requestContent);            
            return dataUrl;            
;
        }

        public async Task<ApiResult<bool>> DeleteImage(DeleteImageRequest request)
        {
            var data = await DeleteAsync<bool>($"/api/News/image/{request.Id}");
            return data;
        }

        public async Task<ApiResult<bool>> DeleteProduct(DeleteProductRequestModel pro)
        {
            var data = await DeleteAsync<bool>($"/api/News/{pro.Id}");           
            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllProduct(ProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductViewModel>>($"/api/News/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");           
            return data;
        }

        public async Task<ImageVM> GetImageById(int id)
        {
            var data = await GetAsync<ImageVM>($"/api/News/image/{id}");
            return data;
        }

        public async Task<List<ImageVM>> GetListImages(int productId)
        {
            var data = await GetAsync<List<ImageVM>>($"/api/News/listImage/{productId}");
            return data;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var data = await GetAsync<ProductViewModel>($"/api/News/{id}");           
            return data;
        }

        public async Task<ApiResult<bool>> SetImage(int productId, SetDefaultImageRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PutAsync<bool>($"/api/News/image/{productId}", httpContent);
            return data;
        }

        public async Task<ApiResult<bool>> UpdateProduct(int id, UpdateProductRequestModel pro)
        {
            var json = JsonConvert.SerializeObject(pro);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PutAsync<bool>($"/api/News/{id}", httpContent);           
            return data;
        }
    }
}
