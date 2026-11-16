using Microsoft.AspNetCore.Authorization;
using EventManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    [AllowAnonymous]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipment;
        public EquipmentController(IEquipmentService equipment) => _equipment = equipment;

        [HttpGet]
        public async Task<IActionResult> GetCatalog() => Ok(await _equipment.GetCatalogAsync());
    }
}
