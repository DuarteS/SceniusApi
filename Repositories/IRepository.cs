using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CalculatorApi
{
    public interface IRepository<Calculation>
    {
        Task CreateAsync(Calculation calculation);
        Task<IReadOnlyCollection<Calculation>> GetAllAsync();
        Task<IReadOnlyCollection<Calculation>> GetAllAsync(Expression<Func<Calculation, bool>> filter);
        Task<Calculation> GetAsync(Guid id);
        Task<Calculation> GetAsync(Expression<Func<Calculation, bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Calculation calculation);
    }
}