using News.ViewModel.Common;
using News.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Admin.Service
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<IEnumerable<UserVM>> GetAllUser();
        Task<UserVM> GetById(Guid id);
        Task<ApiResult<bool>> RegisterUser(UserRegisterRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
    }
}
