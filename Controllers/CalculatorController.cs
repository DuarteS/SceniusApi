using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorApi.Models;
using CalculatorApi.Services;

namespace CalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly CalculationService _calculationService;

        public CalculationController(CalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Calculation calculation)
        {
            await _calculationService.CreateCalculationAsync(calculation);
            return CreatedAtAction(nameof(GetAsync), new { id = calculation.Id }, calculation);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Calculation>>> GetAllAsync()
        {
            var calculations = await _calculationService.GetAllCalculationsAsync();
            return Ok(calculations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calculation>> GetAsync(Guid id)
        {
            var calculation = await _calculationService.GetCalculationByIdAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }
            return Ok(calculation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Calculation calculation)
        {
            if (id != calculation.Id)
            {
                return BadRequest();
            }

            await _calculationService.UpdateCalculationAsync(calculation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _calculationService.RemoveCalculationAsync(id);
            return NoContent();
        }
    }
}