using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.ServiceApiClient;
using News.ViewModel.Common;
using News.ViewModel.System.Role;
using News.ViewModel.System.User;

namespace News.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IRoleApiClient _roleApiClient;

        public UsersController(IUserApiClient userApiClient, IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 6)
        {
            var request = new UserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _userApiClient.GetAllUser(request);

            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Đăng kí thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userApiClient.GetById(id);
            if (user != null)
            {                
                var updateRequest = new UserUpdateRequest()
                {
                    DoB = user.DoB,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber =  int.Parse(user.PhoneNumber),
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {             
                TempData["result"] = "Cập nhật người dùng thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]        
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {               
                TempData["result"] = "Xóa người dùng thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApiClient.RoleAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}