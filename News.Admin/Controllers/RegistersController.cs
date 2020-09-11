using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.ServiceApiClient;
using News.ViewModel.Catalog.Register;

namespace News.Admin.Controllers
{
    public class RegistersController : BaseController
    {       
        private readonly IRegisterApiClient _registerApiClient;

        public RegistersController(IRegisterApiClient registerApiClient)
        {
            _registerApiClient = registerApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 6)
        {
            var request = new RegisterPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _registerApiClient.GetAllRegister(request);

            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult CreateRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegister(CreateRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _registerApiClient.CreateRegister(request);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Thêm mới thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsRegister(int id)
        {
            var result = await _registerApiClient.GetRegisterById(id);
            return View(result);
        }


        [HttpGet]
        public IActionResult DeleteRegister(int id)
        {
            return View(new DeleteRegisterRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRegister(DeleteRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _registerApiClient.DeleteRegister(request);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Xóa thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}