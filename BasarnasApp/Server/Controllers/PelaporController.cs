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
    public class PelaporController : ControllerBase
    {

        private readonly ILogger<PelaporController> _logger;
        private readonly IPelaporService _pelaporService;

        public PelaporController(ILogger<PelaporController> logger, IPelaporService pelaporService)
        {
            _logger = logger;
            _pelaporService = pelaporService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _pelaporService.GetAsync();
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
                var result = await _pelaporService.GetByIdAsync(id);
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
                var result = await _pelaporService.GetProfile(claim.Value);
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
                var result = await _pelaporService.ChangePassword(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(PelaporRequest request)
        {
            try
            {
                var Pelapor = new Pelapor
                {
                    Id = request.Id,
                    Name = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Password = request.Password,
                    Address = request.Address
                };
                var result = await _pelaporService.PostAsync(Pelapor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PelaporRequest request)
        {
            try
            {
                var model = new Pelapor
                {
                    Id = request.Id,
                    Name = request.Name,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Photo = request.Photo,
                    Thumb = request.Thumb
                };

                if (request.PhotoData != null && request.PhotoData.Length > 0)
                {
                    var logo = await Helper.CreatePhotoProfile(request.PhotoData);
                    model.Photo = logo.File;
                    model.Thumb = logo.Thumb;
                }

                var result = await _pelaporService.PutAsync(id, model);
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
                var result = await _pelaporService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
