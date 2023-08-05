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
    public class InstansiController : ControllerBase
    {
     
        private readonly ILogger<InstansiController> _logger;
        private readonly IInstansiService _instansiService ;
        private IWebHostEnvironment _webHostEnvironment ;

        public InstansiController(ILogger<InstansiController> logger, IInstansiService instansiService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _instansiService = instansiService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _instansiService.GetAsync();
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
                var result = await _instansiService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(InstansiRequest request)
        {
            try
            {
                var instansi = new Instansi { Id = request.Id, Name = request.Name, Description = request.Description };
                if (request.DataLogo!=null && request.DataLogo.Length > 0)
                {
                    var logo = await Helper.CreateLogo(request.DataLogo);
                    instansi.Logo = logo.File;
                    instansi.Thumb = logo.Thumb;
                }

                var result = await _instansiService.PostAsync(instansi);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,  InstansiRequest request)
        {
            try
            {
                
                var instansi = new Instansi { Id = request.Id, Name = request.Name,
                         Description = request.Description };
                
                if(request.DataLogo.Length >0){
                   var logo  = await Helper.CreateLogo(request.DataLogo, request.Logo);
                   instansi.Logo = logo.File ;
                   instansi.Thumb = logo.Thumb ;
                }
                
                var result = await _instansiService.PutAsync(id, instansi);
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
                var result = await _instansiService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
