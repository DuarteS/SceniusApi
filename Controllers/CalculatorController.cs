using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorApi.Models;
using CalculatorApi.Services;
using System.Text.Json;

namespace CalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly CalculationService _calculationService;

        public CalculationController(CalculationService calculationService, RabbitMQService rabbitMQService)
        {
            _calculationService = calculationService;
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Calculation calculation)
        {
            var message = JsonSerializer.Serialize<Calculation>(calculation);
            _rabbitMQService.SendMessage(message);
            await _calculationService.CreateCalculationAsync(calculation);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Calculation>>> GetAllAsync()
        {
            var calculations = await _calculationService.GetAllCalculationsAsync();
            return Ok(calculations);
        }
    }
}