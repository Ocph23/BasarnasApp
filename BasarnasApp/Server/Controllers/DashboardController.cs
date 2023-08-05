using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasarnasApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
     
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _DashboardService ;
        public DashboardController(
            ILogger<DashboardController> logger, 
            IDashboardService
            DashboardService)
        {
            _logger = logger;
            _DashboardService = DashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _DashboardService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
