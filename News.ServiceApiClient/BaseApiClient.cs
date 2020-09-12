using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using News.ViewModel.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);                     
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }
            throw new Exception(body);
        }

        protected async Task<ApiResult<TResponse>> PostAsync<TResponse>(string url, HttpContent content)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PostAsync(url, content);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiSuccessResult<TResponse> myDeserializedObjList = (ApiSuccessResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiSuccessResult<TResponse>));
                return myDeserializedObjList;
            }
            ApiErrorResult<TResponse> ErrorMyDeserializedObjList = (ApiErrorResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiErrorResult<TResponse>));
            return ErrorMyDeserializedObjList;
        }

        protected async Task<ApiResult<TResponse>> DeleteAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiSuccessResult<TResponse> myDeserializedObjList = (ApiSuccessResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiSuccessResult<TResponse>));
                return myDeserializedObjList;
            }
            ApiErrorResult<TResponse> ErrorMyDeserializedObjList = (ApiErrorResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiErrorResult<TResponse>));
            return ErrorMyDeserializedObjList;
        }

        protected async Task<ApiResult<TResponse>> PutAsync<TResponse>(string url, HttpContent content)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PutAsync(url, content);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiSuccessResult<TResponse> myDeserializedObjList = (ApiSuccessResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiSuccessResult<TResponse>));
                return myDeserializedObjList;
            }
            ApiErrorResult<TResponse> ErrorMyDeserializedObjList = (ApiErrorResult<TResponse>)JsonConvert.DeserializeObject(body, typeof(ApiErrorResult<TResponse>));
            return ErrorMyDeserializedObjList;
        }
    }
}
