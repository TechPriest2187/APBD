using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityTasksDbFirstApi.DTOs;
using UniversityTasksDbFirstApi.Services;

namespace UniversityTasksDbFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmission([FromBody] CreateSubmissionDto dto)
        {
            var result = await _submissionService.CreateSubmissionAsync(dto);

            if (result.StatusCode == 400) return BadRequest(result.ErrorMessage);
            if (result.StatusCode == 404) return NotFound(result.ErrorMessage);
            if (result.StatusCode == 409) return Conflict(result.ErrorMessage);

            return Created(string.Empty, new { id = result.CreatedId });
        }

        [HttpPut("{idSubmission}/grade")]
        public async Task<IActionResult> GradeSubmission(int idSubmission, [FromBody] GradeSubmissionDto dto)
        {
            var result = await _submissionService.GradeSubmissionAsync(idSubmission, dto);

            if (result.StatusCode == 404) return NotFound(result.ErrorMessage);
            if (result.StatusCode == 400) return BadRequest(result.ErrorMessage);

            return Ok(new { message = "Submission graded successfully." });
        }

        [HttpDelete("{idSubmission}")]
        public async Task<IActionResult> DeleteSubmission(int idSubmission)
        {
            var result = await _submissionService.DeleteSubmissionAsync(idSubmission);

            if (result.StatusCode == 404) return NotFound(result.ErrorMessage);
            if (result.StatusCode == 400) return BadRequest(result.ErrorMessage);

            // Returns HTTP 204 No Content
            return NoContent(); 
        }
    }
}