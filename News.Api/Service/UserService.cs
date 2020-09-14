using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using News.Data.Entities;
using News.ViewModel.Common;
using News.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;          
        }

        public async Task<ApiResult<string>> AuthencateAdmin(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }

            var userRoles = await _userManager.IsInRoleAsync(user, "admin");    
            if (userRoles == false)
            {
                return new ApiErrorResult<string>("Bạn không có quyền đăng nhập");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<PagedResult<UserVM>> GetAllUsers(UserPagingRequest request)
        {
            var query = from u in _userManager.Users

                        select new { u };

            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.u.FirstName.Contains(request.Keyword)
                 || x.u.LastName.Contains(request.Keyword)
                 || x.u.UserName.Contains(request.Keyword)
                 || x.u.Email.Contains(request.Keyword)
                 || x.u.PhoneNumber.Contains(request.Keyword));
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVM()
                {
                    Email = x.u.Email,
                    PhoneNumber = x.u.PhoneNumber,
                    UserName = x.u.UserName,
                    FirstName = x.u.FirstName,
                    Id = x.u.Id,
                    LastName = x.u.LastName,
                    DoB = x.u.DoB
                }).ToListAsync();

            var pagedResult = new PagedResult<UserVM>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedResult;           
        }

        public async Task<UserVM> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());            
            var roles = await _userManager.GetRolesAsync(user);

            var userVm = new UserVM()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                DoB = user.DoB,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles
            };

            return userVm;
        }
       
        public async Task<ApiResult<bool>> RegisterUser(UserRegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            user = new AppUser()
            {
                DoB = request.DoB,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber.ToString()
            };
            var result = await _userManager.CreateAsync(user, request.Password);            
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.DoB = request.DoB;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber.ToString();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
    }
}
