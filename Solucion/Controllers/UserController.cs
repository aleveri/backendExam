using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SB.Entities;
using SB.Interfaces;
using SB.Resources;
using static SB.Entities.Enums;

namespace Solucion
{
    [Produces("application/json")]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserEs _service;

        public UserController(IUserEs service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User param)
        {
            if (param != null)
            {
                if (!ModelState.IsValid) return BadRequest(ErrorCodes.InvalidModel);
                Singleton.Instance.Audit = true;
                var response = await _service.Add(param);
                if (response.Estado) return Ok();
                ModelState.AddModelError("Error", string.Join(",", response.Errores));
                return BadRequest(ModelState);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid param)
        {
            if (!param.Equals(Guid.Empty))
            {
                Singleton.Instance.Audit = false;
                var response = await _service.Get(param);
                if (response.Estado) return Ok(response);
                ModelState.AddModelError("Error", string.Join(",", response.Errores));
                return BadRequest(ModelState);
            }
            return BadRequest();
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int page, int pageSize)
        {
            Singleton.Instance.Audit = false;
            var response = await _service.List(new Pagination() { Page = page, PageSize = pageSize });
            if (response.Estado) return Ok(response);
            ModelState.AddModelError("Error", string.Join(",", response.Errores));
            return BadRequest(ModelState);
        }

        [HttpGet("ListAsync")]
        public async Task<IActionResult> ListAsync(int page, int pageSize, string criterion)
        {
            Singleton.Instance.Audit = false;
            var response = await _service.ListAsync(new Pagination() { Page = page, PageSize = pageSize, Criterion = criterion });
            if (response.Estado) return Ok(response);
            ModelState.AddModelError("Error", string.Join(",", response.Errores));
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User param)
        {
            if (param != null)
            {
                if (!ModelState.IsValid) return BadRequest(ErrorCodes.InvalidModel);
                Singleton.Instance.Audit = true;
                var response = await _service.Update(param);
                if (response.Estado) return Ok();
                ModelState.AddModelError("Error", string.Join(",", response.Errores));
                return BadRequest(ModelState);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid param)
        {
            if (!param.Equals(Guid.Empty))
            {
                var response = await _service.Delete(param);
                if (response.Estado) return Ok();
                ModelState.AddModelError("Error", string.Join(",", response.Errores));
                return BadRequest(ModelState);
            }
            return BadRequest();
        }
    }
}