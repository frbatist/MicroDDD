using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public interface IQueryableHelper
    {
        Task<List<T>> ToListAsync<T>(IQueryable<T> consulta);
        Task<T[]> ToArrayAsync<T>(IQueryable<T> consulta);        
    }
}
