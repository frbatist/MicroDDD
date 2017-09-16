using MicroDDD.Dominio.Identidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Aplicacao.Identidade
{
    public interface IUsuarioAplicacao
    {
        IUsuarioLogado ObterUsuarioLogado();
    }
}
