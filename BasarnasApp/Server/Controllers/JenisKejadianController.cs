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
    public class JenisKejadianController : ControllerBase
    {

        private readonly ILogger<JenisKejadianController> _logger;
        private readonly IJenisKejadianService _JenisKejadianService;

        public JenisKejadianController(ILogger<JenisKejadianController> logger, IJenisKejadianService JenisKejadianService)
        {
            _logger = logger;
            _JenisKejadianService = JenisKejadianService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _JenisKejadianService.GetAsync();

                var data = from a in result
                           select new JenisKejadianRequest
                           {
                               Description = a.Description,
                               Id = a.Id,
                               Name = a.Name,
                               Instansis = a.JenisInstansi.Select(x => new InstansiRequest
                               {
                                   Description = x.Instansi.Description,
                                   Id = x.Instansi.Id,
                                   Name = x.Instansi.Name,
                               }).ToList()
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
                var a = await _JenisKejadianService.GetByIdAsync(id);
                if(a !=null){
                   return Ok(new JenisKejadianRequest
                           {
                               Description = a.Description,
                               Id = a.Id,
                               Name = a.Name,
                               Instansis = a.JenisInstansi.Select(x => new InstansiRequest
                               {
                                   Description = x.Instansi.Description,
                                   Id = x.Instansi.Id,
                                   Name = x.Instansi.Name,
                               }).ToList()
                           });
                }    

                return Ok(a);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(JenisKejadianRequest request)
        {
            try
            {
                var JenisKejadian = new JenisKejadian
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description
                };
                foreach (var item in request.Instansis)
                {
                    var i = new Instansi { Description = item.Description, Id = item.Id, Name = item.Name };
                    JenisKejadian.AddNewInstansi(i);
                }
                var result = await _JenisKejadianService.PostAsync(JenisKejadian);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, JenisKejadianRequest request)
        {
            try
            {
                var JenisKejadian = new JenisKejadian { Id = request.Id, Name = request.Name, Description = request.Description };
                foreach (var item in request.Instansis)
                {
                    var i = new Instansi { Description = item.Description, Id = item.Id, Name = item.Name };
                    JenisKejadian.AddNewInstansi(i);
                }

                var result = await _JenisKejadianService.PutAsync(id, JenisKejadian);
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
                var result = await _JenisKejadianService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
