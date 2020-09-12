
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using News.ViewModel.Catalog.Register;
using News.ViewModel.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public class RegisterApiClient : BaseApiClient, IRegisterApiClient
    {       
        public RegisterApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            
        }

        public async Task<ApiResult<bool>> CreateRegister(CreateRegisterRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PostAsync<bool>($"/api/Registers", httpContent);        
            return data;
        }

        public async Task<ApiResult<bool>> DeleteRegister(DeleteRegisterRequest request)
        {
            var data = await DeleteAsync<bool>($"/api/Registers/{request.Id}");
            return data;
        }

        public async Task<PagedResult<RegisterViewModel>> GetAllRegister(RegisterPagingRequest request)
        {
            var data = await GetAsync<PagedResult<RegisterViewModel>>($"/api/Registers/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");           
            return data;
        }

        public async Task<RegisterViewModel> GetRegisterById(int id)
        {
            var data = await GetAsync<RegisterViewModel>($"/api/Registers/{id}");            
            return data;
        }
    }
}
