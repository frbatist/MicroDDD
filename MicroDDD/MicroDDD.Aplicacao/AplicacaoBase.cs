﻿using MicroDDD.Aplicacao.Identidade;
using MicroDDD.Dominio.Repositorio;
using MicroDDD.Dominio.Entidade;
using System.Threading.Tasks;

namespace MicroDDD.Aplicacao
{
    public class AplicacaoBase : IAplicacao
    {
        public IUsuarioAplicacao UsuarioAplicacao { get; set; }
        private IUnidadeTrabalho _unidadeTrabalho;

        public AplicacaoBase(IUnidadeTrabalho unidadeTrabalho)
        {
            _unidadeTrabalho = unidadeTrabalho;
        }

        public async Task SalvarAlteracoes()
        {
            await _unidadeTrabalho.FinalizarAsync();
        }

        public void Dispose()
        {
            _unidadeTrabalho.Dispose();
        }
    }
}
