using News.Data.Entities;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public interface IRegisterService
    {
        Task<IEnumerable<Register>> GetAllRegister();
        Task<Register> GetRegisterById(int id);
        Task<ApiResult<bool>> CreateRegister(Register reg);       
        Task<ApiResult<bool>> DeleteRegister(Register reg);
    }
}
