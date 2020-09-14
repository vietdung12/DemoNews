using News.ViewModel.Common;
using News.ViewModel.System.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public interface IRoleService 
    {
        Task<List<RoleVM>> GetAll();
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
