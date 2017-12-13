using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDDD.WebApi
{
    public class ApiControllerBase : ApiController
    {
        protected async Task<IHttpActionResult> Executa<T>(Func<Task<T>> action)
        {
            try
            {
                return Ok<T>(await action());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected async Task<IHttpActionResult> Executa(Func<Task> action)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
