﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Api.Service;
using News.Data.Entities;
using News.ViewModel.Catalog.Product;
using News.ViewModel.Common;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;           
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductPagingRequest request)
        {
            var Items = await _newsService.GetAllProduct(request);            
            return Ok(Items);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int id)
        {
            var Items = await _newsService.GetProductById(id);
            
            if (Items != null)
            {
                return Ok(Items);
            }
            return NotFound();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductViewModel>> CreateProduct([FromForm]CreateProductRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _newsService.CreateProduct(requestModel);
            if (productId == 0)
                return BadRequest();

            var product = await _newsService.GetProductById(productId);
            //return Ok(productModel);
            return CreatedAtRoute(nameof(GetProductById), new { id = productId }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resut = await _newsService.UpdateProduct(id, requestModel);
            return Ok(resut);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {            
            var resut = await _newsService.DeleteProduct(id);
            return Ok(resut);
        }
    }
}