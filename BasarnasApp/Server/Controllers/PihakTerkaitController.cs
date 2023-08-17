using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasarnasApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PihakTerkaitController : ControllerBase
    {

        private readonly ILogger<PihakTerkaitController> _logger;
        private readonly IPihakTerkaitService _pihakTerkaitService;

        public PihakTerkaitController(ILogger<PihakTerkaitController> logger, IPihakTerkaitService DistrictService)
        {
            _logger = logger;
            _pihakTerkaitService = DistrictService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _pihakTerkaitService.GetAsync();
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
                var result = await _pihakTerkaitService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var claim = User.Claims.Where(x => x.Type == "id").FirstOrDefault();
                ArgumentNullException.ThrowIfNull(claim, "Anda tidak memiliki akses !");
                var result = await _pihakTerkaitService.GetProfile(claim.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PihakTerkaitRequest request)
        {
            try
            {
                var pihakTerkait = new PihakTerkait
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    District = new District { Id = request.District.Id },
                    Instansi = new Instansi { Id = request.Instansi.Id },
                    Email = request.Email,
                    UserId = request.UserId,

                };
                var result = await _pihakTerkaitService.PostAsync(pihakTerkait , request.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PihakTerkaitRequest request)
        {
            try
            {
                var pihakTerkait = new PihakTerkait
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    District = new District { Id = request.District.Id },
                    Instansi = new Instansi { Id = request.Instansi.Id },
                    ProfileName = request.ProfileName,
                    ProfileAddress = request.ProfileAddress,
                    ProfileJabatan = request.ProfileJabatan,
                    Email = request.Email,
                    UserId = request.UserId,

                };
                var result = await _pihakTerkaitService.PutAsync(id, pihakTerkait);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, ChangeUserPasswordRequest request)
        {
            try
            {
                var result = await _pihakTerkaitService.ChangePassword(id, request);
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
                var result = await _pihakTerkaitService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
