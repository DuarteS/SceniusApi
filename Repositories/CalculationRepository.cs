using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CalculatorApi.Data;
using CalculatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApi.Repositories
{
    public class CalculationRepository : IRepository<Calculation>
    {
        private readonly AppDbContext _context;

        public CalculationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Calculation calculation)
        {
            await _context.Calculations.AddAsync(calculation);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Calculation>> GetAllAsync()
        {
            return await _context.Calculations.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Calculation>> GetAllAsync(Expression<Func<Calculation, bool>> filter)
        {
            return await _context.Calculations.Where(filter).ToListAsync();
        }

        public async Task<Calculation> GetAsync(Guid id)
        {
            return await _context.Calculations.FindAsync(id);
        }

        public async Task<Calculation> GetAsync(Expression<Func<Calculation, bool>> filter)
        {
            return await _context.Calculations.FirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(Guid id)
        {
            var calculation = await GetAsync(id);
            if (calculation != null)
            {
                _context.Calculations.Remove(calculation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Calculation calculation)
        {
            _context.Calculations.Update(calculation);
            await _context.SaveChangesAsync();
        }
    }
}