using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        private ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
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

            _logger.LogError($"{exception.Message}, {exception.StackTrace}");

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(exception.Message);
        }
    }
}
