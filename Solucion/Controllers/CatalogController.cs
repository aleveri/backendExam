using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SB.Entities;
using SB.Interfaces;
using SB.Resources;

namespace Solucion.Controllers
{
    [Produces("application/json")]
    [Route("Catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogEs _service;

        public CatalogController(ICatalogEs service) => _service = service;

        [HttpGet("ListByType")]
        public async Task<IActionResult> ListByType(int page, int pageSize, string type)
        {
            Singleton.Instance.Audit = false;
            var response = await _service.ListByType(new Pagination() { Page = page, PageSize = pageSize, Criterion = type });
            if (response.Estado) return Ok(response);
            ModelState.AddModelError("Error", string.Join(",", response.Errores));
            return BadRequest(ModelState);
        }

        [HttpGet("ListByParent")]
        public async Task<IActionResult> ListByParent(int page, int pageSize, string parent)
        {
            Singleton.Instance.Audit = false;
            var response = await _service.ListByParent(new Pagination() { Page = page, PageSize = pageSize, Criterion = parent });
            if (response.Estado) return Ok(response);
            ModelState.AddModelError("Error", string.Join(",", response.Errores));
            return BadRequest(ModelState);
        }
    }
}