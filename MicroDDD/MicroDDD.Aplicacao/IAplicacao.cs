using MicroDDD.Aplicacao.Identidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Aplicacao
{
    public interface IAplicacao : IDisposable
    {
        IUsuarioAplicacao UsuarioAplicacao { get; set; }
    }
}
