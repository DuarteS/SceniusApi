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
    }
}