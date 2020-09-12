using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using News.ViewModel.Common;
using News.ViewModel.System.User;
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
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
             : base(httpClientFactory, httpContextAccessor, configuration)
        {

        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {           
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PostAsync<string>("/api/users/authenticate", httpContent);            
            return data;
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var data = await DeleteAsync<bool>($"/api/Users/{id}");
            return data;
        }

        public async Task<PagedResult<UserVM>> GetAllUser(UserPagingRequest request)
        {
            var data = await GetAsync<PagedResult<UserVM>>($"/api/Users/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            return data;
        }

        public async Task<UserVM> GetById(Guid id)
        {
            var data = await GetAsync<UserVM>($"/api/Users/{id}");          
            return data;
        }

        public async Task<ApiResult<bool>> RegisterUser(UserRegisterRequest request)
        {            
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PostAsync<bool>($"/api/Users", httpContent);           
            return data;
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {           
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PutAsync<bool>($"/api/users/{id}", httpContent);           
            return data;
        }
    }
}
