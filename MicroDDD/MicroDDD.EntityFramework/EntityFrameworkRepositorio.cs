using MicroDDD.Dominio.Entidade;
using MicroDDD.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace MicroDDD.EntityFramework
{
    public class EntityFrameworkRepositorio<T> : IRepositorio<T> where T : class, IEntidade
    {
        private DbContext _contexto;

        public EntityFrameworkRepositorio(DbContext contexto)
        {
            _contexto = contexto;
        }

        public void AdicionarLote(IEnumerable<T> entidades)
        {
            _contexto.AddRange(entidades);
        }

        public Task AdicionarLoteAsync(IEnumerable<T> entidades)
        {
            return _contexto.AddRangeAsync(entidades);
        }

        public void AdicionarOuAtualizar(params T[] entidades)
        {
            foreach (var entidade in entidades)
            {
                if (_contexto.Entry<T>(entidade).State == EntityState.Detached)
                {
                    _contexto.Attach(entidade);
                }
                else
                {
                    _contexto.Update(entidade);
                }
            }
        }

        public Task AdicionarOuAtualizarAsync(params T[] entidades)
        {
            return Task.Run(() => 
            {
                AdicionarOuAtualizar(entidades);
            });
        }

        public int ExecutaComandoSql(IEnumerable<ParametroComandoSql> parametros, string comando)
        {
            return _contexto.Database.ExecuteSqlRaw(comando, parametros.Select
                (
                    d => new SqlParameter
                    {
                        ParameterName = d.Nome,
                        DbType = d.Tipo,
                        Value = d.Valor,
                        Size = d.Tamanho,
                        Precision = d.Precisao
                    }
            ));
        }

        public Task<int> ExecutaComandoSqlAsync(IEnumerable<ParametroComandoSql> parametros, string comando)
        {
            return _contexto.Database.ExecuteSqlRawAsync(comando, parametros.Select
                    (
                        d => new SqlParameter
                        {
                            ParameterName = d.Nome,
                            DbType = d.Tipo,
                            Value = d.Valor,
                            Size = d.Tamanho,
                            Precision = d.Precisao
                        }
            ));
        }

        public T ObterPorId<TId>(TId id)
        {
            return _contexto.Set<T>().Find(id);
        }

        public ValueTask<T> ObterPorIdAsync<TId>(TId id)
        {
            return _contexto.Set<T>().FindAsync(id);
        }

        public IQueryable<T> ObterTodos(Expression<Func<T, bool>> filtro = null, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> consulta = _contexto.Set<T>();
            foreach (var item in include)
            {
                consulta = consulta.Include(item);
            }

            if (filtro != null)
                consulta = consulta.Where(filtro);
            return consulta;
        }

        public void Remover(params T[] entidades)
        {
            _contexto.RemoveRange(entidades);
        }

        public Task RemoverAsync(params T[] entidades)
        {
            return Task.Run(() =>
            {
                _contexto.RemoveRange(entidades);
            });
        }

        void IRepositorioConsultavel<T>.CarregarReferencia<P>(T entidade, Expression<Func<T, IEnumerable<P>>> propriedadeLista)
        {
            _contexto.Entry(entidade).Collection(propriedadeLista);
        }

        void IRepositorioConsultavel<T>.CarregarReferencia<P>(T entidade, Expression<Func<T, P>> propriedade)
        {
            _contexto.Entry(entidade).Reference(propriedade);
        }
    }
}
