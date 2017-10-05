using MicroDDD.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroDDD.Dominio.Repositorio;
using TesteWebApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TesteWebApi.Aplicacao
{
    public class AutorAplicacao : AplicacaoBase, IAutorAplicacao
    {
        IRepositorio<Autor> _repositorioAutor;
        public AutorAplicacao(IUnidadeTrabalho unidadeTrabalho, IRepositorio<Autor> repositorioAutor) : base(unidadeTrabalho)
        {
            _repositorioAutor = repositorioAutor;
        }

        public async Task<IEnumerable<Autor>> ObterTodos()
        {
            return await _repositorioAutor.ObterTodos().ToListAsync();
        }
    }
}
