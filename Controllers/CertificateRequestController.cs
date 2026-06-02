using CertificatesApp.DTO;
using CertificatesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateRequestController : ControllerBase
    {
        private readonly ICertificateService _service;

        public CertificateRequestController(ICertificateService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CertificateRequestDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CertificateRequestDto>> Create(
            [FromBody] CreateCertificateRequestDto dto)
        {
            var result = await _service.CreateRequestAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("employee/{employeeId:guid}")]
        [ProducesResponseType(typeof(List<CertificateRequestDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CertificateRequestDto>>> GetByEmployee(Guid employeeId)
        {
            var request = await _service.GetByEmployeeAsync(employeeId);
            return Ok(request);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CertificateRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CertificateRequestDto>> GetById(Guid id)
        {
            var request = await _service.GetByIdAsync(id);
            return Ok(request);
        }

        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(typeof(CertificateRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CertificateRequestDto>> UpdateStatus(Guid id, [FromBody] UpdateStatusDto dto)
        {
            var request = await _service.UpdateStatusAsync(id, dto);
            return Ok(request);
        }
    }
}
