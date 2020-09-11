using News.ViewModel.Catalog.Register;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public interface IRegisterApiClient
    {
        Task<PagedResult<RegisterViewModel>> GetAllRegister(RegisterPagingRequest request);
        Task<RegisterViewModel> GetRegisterById(int id);
        Task<ApiResult<bool>> CreateRegister(CreateRegisterRequest request);
        Task<ApiResult<bool>> DeleteRegister(DeleteRegisterRequest request);
    }
}
