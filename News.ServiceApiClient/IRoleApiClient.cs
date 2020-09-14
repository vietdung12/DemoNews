using News.ViewModel.Common;
using News.ViewModel.System.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.ServiceApiClient
{
    public interface IRoleApiClient
    {
        Task<List<RoleVM>> GetAll();
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
