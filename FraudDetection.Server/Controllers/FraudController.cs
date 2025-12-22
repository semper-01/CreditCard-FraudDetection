using Microsoft.AspNetCore.Mvc;
using FraudDetection.Shared.DTOs;
using FraudDetection.Shared.Contracts;

namespace FraudDetection.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FraudController : ControllerBase
    {
        private readonly IFraudService _service;
        private readonly ILogger<FraudController> _logger;

        public FraudController(IFraudService service, ILogger<FraudController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("score")]
        public async Task<IActionResult> Score([FromBody] TransactionDto tx)
        {
            if (tx == null) return BadRequest("Transaction body is required.");
            var result = await _service.ScoreAsync(tx);
            if (result == null)
            {
                _logger.LogWarning("Score returned null for transaction.");
                return StatusCode(502, "Upstream service error");
            }
            return Ok(result);
        }

        [HttpPost("explain")]
        public async Task<IActionResult> Explain([FromBody] TransactionDto tx)
        {
            if (tx == null) return BadRequest("Transaction body is required.");
            var result = await _service.ExplainAsync(tx);
            if (result == null)
            {
                _logger.LogWarning("Explain returned null for transaction.");
                return StatusCode(502, "Upstream service error");
            }
            return Ok(result);
        }
    }
}