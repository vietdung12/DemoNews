using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Api.Service;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProducts()
        {
            var Items = await _newsService.GetAllProduct();
            var productModel = _mapper.Map<IEnumerable<ProductViewModel>>(Items);
            return Ok(productModel);

        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int id)
        {
            var Items = await _newsService.GetProductById(id);
            var productModel = _mapper.Map<ProductViewModel>(Items);
            if (Items != null)
            {
                return Ok(productModel);
            }
            return NotFound();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductViewModel>> CreateProduct([FromForm]CreateProductRequestModel requestModel)
        {
            var Model = _mapper.Map<Product>(requestModel);
            await _newsService.CreateProduct(Model, requestModel.Image);

            var productModel = _mapper.Map<ProductViewModel>(Model);
            //return Ok(productModel);
            return CreatedAtRoute(nameof(GetProductById), new { id = productModel.Id }, productModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductRequestModel requestModel)
        {
            var Item = await _newsService.GetProductById(id);
            if (Item == null)
            {
                return NotFound();
            }
            _mapper.Map(requestModel, Item);
            var resut = await _newsService.UpdateProduct(Item);
            return Ok(resut);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var Item = await _newsService.GetProductById(id);
            if (Item == null)
            {
                return NotFound();
            }
            var resut = await _newsService.DeleteProduct(Item);
            return Ok(resut);
        }
    }
}