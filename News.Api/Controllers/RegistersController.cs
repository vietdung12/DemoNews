using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class RegistersController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public RegistersController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterViewModel>>> GetAll()
        {
            var item = await _registerService.GetAllRegister();
            var model = _mapper.Map<IEnumerable<RegisterViewModel>>(item);
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<RegisterViewModel>> GetById(int id)
        {
            var item = await _registerService.GetRegisterById(id);
            var model = _mapper.Map<RegisterViewModel>(item);
            if (item != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<RegisterViewModel>>> Create(CreateRegisterRequest request)
        {
            var itemModel = _mapper.Map<Register>(request);
            await _registerService.CreateRegister(itemModel);

            var model = _mapper.Map<RegisterViewModel>(itemModel);
            return CreatedAtRoute(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _registerService.GetRegisterById(id);
            if(item == null)
            {
                return NotFound();
            }
            var delete = await _registerService.DeleteRegister(item);
            return Ok(delete);
        }
    }
}