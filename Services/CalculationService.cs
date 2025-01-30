using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorApi.Models;
using CalculatorApi.Repositories;

namespace CalculatorApi.Services
{
    public class CalculationService
    {
        private readonly IRepository<Calculation> _calculationRepository;
        private readonly RabbitMQService _rabbitMQService;

        public CalculationService(IRepository<Calculation> calculationRepository, RabbitMQService rabbitMQService)
        {
            _calculationRepository = calculationRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task CreateCalculationAsync(Calculation calculation)
        {
            await _calculationRepository.CreateAsync(calculation);
            _rabbitMQService.SendMessage($"Calculation created: {calculation.Id}");
        }

        public async Task<IReadOnlyCollection<Calculation>> GetAllCalculationsAsync()
        {
            return await _calculationRepository.GetAllAsync();
        }

        public async Task<Calculation> GetCalculationByIdAsync(Guid id)
        {
            return await _calculationRepository.GetAsync(id);
        }

        public async Task UpdateCalculationAsync(Calculation calculation)
        {
            await _calculationRepository.UpdateAsync(calculation);
        }

        public async Task RemoveCalculationAsync(Guid id)
        {
            await _calculationRepository.RemoveAsync(id);
        }
    }
}