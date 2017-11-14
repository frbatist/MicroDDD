using MicroDDD.Infra.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroDDD.EntityFramework
{
    public class QueryableHelper : IQueryableHelper
    {
        public Task<T[]> ToArrayAsync<T>(IQueryable<T> consulta)
        {
            return consulta.ToArrayAsync();
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> consulta)
        {
            return consulta.ToListAsync();
        }
    }
}
