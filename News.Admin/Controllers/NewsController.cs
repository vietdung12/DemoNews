using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.ServiceApiClient;
using News.ViewModel.Catalog.Image;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;

namespace News.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IProductApiClient _productApiClient;

        public NewsController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 6)
        {
            var request = new ProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _productApiClient.GetAllProduct(request);

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]CreateProductRequestModel pro)
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
            return View(new DeleteProductRequestModel()
            {
                Id = id
            });
        }

        [HttpPost]        
        public async Task<IActionResult> Delete(DeleteProductRequestModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteProduct(request);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Xóa thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public IActionResult AddImages(int productId)
        {
            return View(new AddImageRequest()
            {
                ProductId = productId
            });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImages([FromForm]AddImageRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.AddImage(request);
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
        public async Task<IActionResult> GetListImage(int productId)
        {
            var listImage = await _productApiClient.GetListImages(productId);              
            return View(listImage);  
        }

        [HttpGet]
        public IActionResult DeleteImages(int id)
        {
            return View(new DeleteImageRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImages(DeleteImageRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteImage(request);
            if (result.IsSuccessed)
            {
                //thông báo Action result cho trang Index
                TempData["result"] = "Xóa thành công";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ImageDetails(int id)
        {
            var result = await _productApiClient.GetImageById(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> SetImage(int productId)
        {           
            var listImage = await _productApiClient.GetListImages(productId);
            var setDefaultImageRequest = new SetDefaultImageRequest();
            foreach (var images in listImage)
            {
                setDefaultImageRequest.Images.Add(new SelectItem()
                {
                    Id = images.Id.ToString(),
                    Name = images.ImagePath,
                    Selected = images.IsDefault
                });
            }
            return View(setDefaultImageRequest);
        }

        [HttpPost]
        public async Task<IActionResult> SetImage(SetDefaultImageRequest request)
        {
            if(!ModelState.IsValid)
                return View();

            foreach(var image in request.Images) 
            { 
                if(image.Selected == true) 
                {
                    var result = await _productApiClient.SetImage(request.ProductId, request);

                    if (result.IsSuccessed)
                    {
                        TempData["result"] = "Cập nhật thành công";
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", result.Message);                    
                    return View(request);
                }
            }
            ModelState.AddModelError("", "Phải có 1 hình default");
            return View(request);
        }
        
    }
}