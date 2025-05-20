using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Handlers.interfaces;
using Questao5.Application.Queries.Requests.Saldo;
using Questao5.Application.Queries.Responses.Saldo;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("saldo")]
    public class SaldoContaController : ControllerBase
    {

        private readonly ISaldoHandler saldoHanler;
        public SaldoContaController(ISaldoHandler handler)
        {
            this.saldoHanler = handler;
        }

        [HttpGet]
        [Route("/{idConta}")]
        public async Task<SaldoContaQueryOutput> GetSaldoConta(string idConta)
        {
            var saldoInput = new SaldoContaQuery
            {
                IdContaCorrente = idConta,
            };
            var result = this.saldoHanler.Handle(saldoInput);

            return result;
        }
    }
}