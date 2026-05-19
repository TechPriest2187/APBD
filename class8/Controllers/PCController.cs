using APBD_TASK_7.DTOs;
using APBD_TASK_7.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_TASK_7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PCsController : ControllerBase
    {
        private readonly IPcService _pcService;

        public PCsController(IPcService pcService)
        {
            _pcService = pcService;
        }

        // GET api/pcs
        [HttpGet]
        public async Task<IActionResult> GetPcs()
        {
            var pcs = await _pcService.GetAllPcsAsync();
            return Ok(pcs); // 200 OK [cite: 141]
        }

        // GET api/pcs/{id}/components
        [HttpGet("{id}/components")]
        public async Task<IActionResult> GetPcComponents(int id)
        {
            var components = await _pcService.GetPcComponentsAsync(id);
            
            if (components == null)
            {
                return NotFound(); // 404 Not Found if computer does not exist [cite: 93, 146]
            }

            return Ok(components); // 200 OK
        }

        // POST api/pcs
        [HttpPost]
        public async Task<IActionResult> CreatePc([FromBody] PcCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request on incorrect input [cite: 144]
            }

            var createdPc = await _pcService.CreatePcAsync(dto);
            
            return CreatedAtAction(nameof(GetPcs), new { id = createdPc.Id }, createdPc); 
        }

        // PUT api/pcs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePc(int id, [FromBody] PcUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request [cite: 144]
            }

            var success = await _pcService.UpdatePcAsync(id, dto);
            
            if (!success)
            {
                return NotFound(); // 404 Not Found [cite: 146]
            }

            return Ok(); // 200 OK for successful modification [cite: 141]
        }

        // DELETE api/pcs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePc(int id)
        {
            var success = await _pcService.DeletePcAsync(id);
            
            if (!success)
            {
                return NotFound(); // 404 Not Found if computer doesn't exist [cite: 127]
            }

            return NoContent(); // 204 No Content upon correct deletion [cite: 127, 143]
        }
    }
}