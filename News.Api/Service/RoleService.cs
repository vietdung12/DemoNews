using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using News.Data.Entities;
using News.ViewModel.Common;
using News.ViewModel.System.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<RoleVM>> GetAll()
        {
            var roles = await _roleManager.Roles
                 .Select(x => new RoleVM()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description
                 }).ToListAsync();

            return roles;
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }
    }
}
