using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BasarnasApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController : ControllerBase
    {
     
        private readonly ILogger<DistrictController> _logger;
        private readonly IDistrictService _DistrictService ;
        IHubContext<BasarnasHub> _hubcontext;
        public DistrictController(
            ILogger<DistrictController> logger, 
            IDistrictService
            DistrictService,
            IHubContext<BasarnasHub> hubcontext)
        {
            _logger = logger;
            _DistrictService = DistrictService;
            _hubcontext = hubcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _DistrictService.GetAsync();
                await _hubcontext.Clients.All.SendAsync("ReceiveMessage", "test", "test");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _DistrictService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DistrictRequest request)
        {
            try
            {
                var District = new District { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _DistrictService.PostAsync(District);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,  DistrictRequest request)
        {
            try
            {
                var District = new District { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _DistrictService.PutAsync(id, District);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _DistrictService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
