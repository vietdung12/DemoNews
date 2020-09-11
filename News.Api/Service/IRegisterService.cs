using News.Data.Entities;
using News.ViewModel.Catalog.Register;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public interface IRegisterService
    {
        Task<PagedResult<RegisterViewModel>> GetAllRegister(RegisterPagingRequest request);
        Task<RegisterViewModel> GetRegisterById(int id);
        Task<int> CreateRegister(CreateRegisterRequest request);       
        Task<ApiResult<bool>> DeleteRegister(int id);
    }
}
