using AutoMapper;
using Microsoft.EntityFrameworkCore;
using News.Data.EF;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Catalog.Register;
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
        private readonly IMapper _mapper;

        public RegisterService(NewsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateRegister(CreateRegisterRequest request)
        {
            var Model = _mapper.Map<Register>(request);

            _context.Registers.Add(Model);
            await _context.SaveChangesAsync();
            return Model.Id;
        }
    

        public async Task<ApiResult<bool>> DeleteRegister(int id)
        {
            var register = await _context.Registers.FindAsync(id);
            if (register == null)
            {
                return new ApiErrorResult<bool>("Bản ghi không tồn tại");
            }
            _context.Registers.Remove(register);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<RegisterViewModel>> GetAllRegister(RegisterPagingRequest request)
        {
            var query = from r in _context.Registers
                        
                        select new { r };
        
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.r.Name.Contains(request.Keyword)
                 || x.r.Email.Contains(request.Keyword)
                 || x.r.Telephone.Contains(request.Keyword));                 
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.r.DateCreated).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RegisterViewModel()
                {
                    Id = x.r.Id,
                    Name = x.r.Name,
                    Email = x.r.Email,
                    Telephone = x.r.Telephone,
                    IdProduct = x.r.IdProduct,
                    DateCreated = x.r.DateCreated
                }).ToListAsync();

            var pagedResult = new PagedResult<RegisterViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;
        }

        public async Task<RegisterViewModel> GetRegisterById(int id)
        {
            var item = await _context.Registers.FirstOrDefaultAsync(r => r.Id == id);
            var model = _mapper.Map<RegisterViewModel>(item);
            return model;
        }
    }
}
