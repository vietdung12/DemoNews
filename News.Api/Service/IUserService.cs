using News.ViewModel.Common;
using News.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<IEnumerable<UserVM>> GetAllUsers();
        Task<UserVM> GetUserById(Guid id);
        Task<ApiResult<bool>> RegisterUser(UserRegisterRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(Guid id);
    }
}
