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
        

        public CalculationService(IRepository<Calculation> calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        public async Task CreateCalculationAsync(Calculation calculation)
        {
            await _calculationRepository.CreateAsync(calculation);
            
        }

        public async Task<IReadOnlyCollection<Calculation>> GetAllCalculationsAsync()
        {
            return await _calculationRepository.GetAllAsync();
        }
        }
    }
