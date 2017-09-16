using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Dominio.Identidade
{
    public class UsuarioLogado : IUsuarioLogado
    {
        public long Id { get; set; }
        public string Login { get; set; }
    }
}
