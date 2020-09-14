using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using News.ViewModel.Common;
using News.ViewModel.System.Role;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public class RoleApiClient : BaseApiClient, IRoleApiClient
    {
        public RoleApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {

        }

        public async Task<List<RoleVM>> GetAll()
        {
            var data = await GetAsync<List<RoleVM>>($"/api/roles");
            return data;
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PutAsync<bool>($"/api/roles/{id}", httpContent);
            return data;
        }
    }
}
