using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteWebApi.Aplicacao;
using TesteWebApi.Entidades;

namespace TesteWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Autor")]
    public class AutorController : Controller
    {
        private IAutorAplicacao _autorAplicacao;
        public AutorController(IAutorAplicacao autorAplicacao)
        {
            _autorAplicacao = autorAplicacao;
        }

        public async Task<IEnumerable<Autor>> Get()
        {
            return await _autorAplicacao.ObterTodos();
        }
    }
}