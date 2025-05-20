using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests.Movimentacao;
using Questao5.Application.Commands.Responses.Movimentacao;
using Questao5.Application.Handlers.interfaces;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacaoContaController : ControllerBase
    {
        private readonly IMovimentacaoHandler _movimentacaoHandler;
        public MovimentacaoContaController(IMovimentacaoHandler movimento)
        { 
            this._movimentacaoHandler = movimento;
        }

        [HttpPost]
        [Route("movimentar_conta")]
        public async Task<MovimentoOutput> MovimentarConta(MovimentoCommand input)
        {
            var result = this._movimentacaoHandler.Handle(input); 

            return result;
        }
    }
}
