using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Api.Service;
using News.Data.Entities;
using News.ViewModel.Catalog.Register;
using News.ViewModel.Common;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistersController : ControllerBase
    {
        private readonly IRegisterService _registerService;      

        public RegistersController(IRegisterService registerService)
        {
            _registerService = registerService;   
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAll([FromQuery]RegisterPagingRequest request)
        {
            var item = await _registerService.GetAllRegister(request);    
            return Ok(item);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<RegisterViewModel>> GetById(int id)
        {
            var Items = await _registerService.GetRegisterById(id);

            if (Items != null)
            {
                return Ok(Items);
            }
            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterViewModel>> Create(CreateRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registerId = await _registerService.CreateRegister(request);
            if (registerId == 0)
                return BadRequest();

            var register = await _registerService.GetRegisterById(registerId);
            
            return CreatedAtRoute(nameof(GetById), new { id = registerId }, register);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resut = await _registerService.DeleteRegister(id);
            return Ok(resut);
        }
    }
}