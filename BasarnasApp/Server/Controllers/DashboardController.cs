using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            ILogger<DashboardController> logger,
            IDashboardService
            DashboardService,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _DashboardService = DashboardService;
            _userManager = userManager;
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

        [AllowAnonymous]
        [HttpGet("reset/{id}")]
        public async Task<IActionResult> ResetPassword(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var a = await _userManager.ResetPasswordAsync(user, token, "Password@123");
            return Ok(a);
        }

    }
}
