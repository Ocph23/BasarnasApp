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
    public class KejadianController : ControllerBase
    {

        private readonly ILogger<KejadianController> _logger;
        private readonly IKejadianService _kejadianService;

        public KejadianController(ILogger<KejadianController> logger, IKejadianService kejadianService)
        {
            _logger = logger;
            _kejadianService = kejadianService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == "id");
                var result = await _kejadianService.GetByUserAsync(claim.Value);

                var data = from a in result
                           select new KejadianRequest
                           {
                               DistrictId = a.District.Id,
                               DistrictName = a.District.Name,
                               Id = a.Id,
                               JenisKejadianId = a.JenisKejadian.Id,
                               JenisKejadianName = a.JenisKejadian.Name,
                               LongLat = a.LongLat,
                               PelaporId = a.Pelapor.Id,
                               PelaporName = a.Pelapor.Name,
                               Photo = a.Photo,
                               Status = a.Status,
                               Tanggal = a.Tanggal,

                           };



                return Ok(data);
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
                var result = await _kejadianService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(KejadianRequest request)
        {
            try
            {
                var model = new Kejadian
                {
                    Id = request.Id,
                    Photo = request.Photo,
                    Tanggal = request.Tanggal.ToUniversalTime(),
                    LongLat = request.LongLat,
                    Pelapor = new Pelapor { Id = request.PelaporId },
                    District =
                     new District
                     {
                         Id = request.DistrictId,
                     },
                    JenisKejadian =
                     new JenisKejadian
                     {
                         Id = request.JenisKejadianId,
                     }
                };
                var result = await _kejadianService.PostAsync(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, KejadianRequest request)
        {
            try
            {
                var model = new Kejadian
                {
                    Id = request.Id,
                    Photo = request.Photo,
                    Tanggal = request.Tanggal,
                    LongLat = request.LongLat,
                    Pelapor = new Pelapor { Id = request.PelaporId },
                    District =
                     new District
                     {
                         Id = request.DistrictId,
                     },
                    JenisKejadian =
                     new JenisKejadian
                     {
                         Id = request.JenisKejadianId,
                     }
                };
                var result = await _kejadianService.PutAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous ]
        [HttpGet("changestatus/{id}/{status}")]
        public async Task<IActionResult> Changestatus(int id, int status)
        {
            try
            {
                
                bool result = await _kejadianService.ChangeStatus(id, (StatusLaporan)status);
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
                var result = await _kejadianService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
