using Microsoft.EntityFrameworkCore;
using News.Data.EF;
using News.Data.Entities;
using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly NewsDbContext _context;

        public RegisterService(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateRegister(Register reg)
        {
            if (reg == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
            _context.Registers.Add(reg);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteRegister(Register reg)
        {
            if (reg == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
            _context.Registers.Remove(reg);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<IEnumerable<Register>> GetAllRegister()
        {
            var item = await _context.Registers.ToListAsync();
            return item;
        }

        public async Task<Register> GetRegisterById(int id)
        {
            var item = await _context.Registers.FirstOrDefaultAsync(r => r.Id == id);
            return item;
        }
    }
}
