using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Admin.Service;
using News.ViewModel.Catalog.Product;

namespace News.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public NewsController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _productApiClient.GetAllProduct();

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
        public async Task<IActionResult> Create(CreateProductRequestModel pro)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.CreateProduct(pro);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Thêm mới thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(pro);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productApiClient.GetProductById(id);
            if (product != null)
            {               
                var updateRequest = new UpdateProductRequestModel()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Local = product.Local,
                    Description = product.Description,
                    Price = product.Price,
                    DateCreated = DateTime.Now,
                    Status = product.Status                   
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequestModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.UpdateProduct(request.Id, request);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Cập nhật thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productApiClient.GetProductById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DeleteProductRequsetModel()
            {
                Id = id
            });
        }

        [HttpPost]        
        public async Task<IActionResult> Delete(DeleteProductRequsetModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteProduct(request.Id);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Xóa người dùng thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}