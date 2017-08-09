using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is RecursoNaoEncontradoException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is RequisicaoInvalidaException)
            {
                code = (HttpStatusCode)422; //Unprocessable Entity
            }

            else if (exception is ProcessoEletronicoException)
            {
                code = HttpStatusCode.InternalServerError;
            }
            
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(exception.Message);
        }
    }
}
