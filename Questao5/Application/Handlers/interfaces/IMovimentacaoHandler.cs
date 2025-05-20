using Questao5.Application.Commands.Requests.Movimentacao;
using Questao5.Application.Commands.Responses.Movimentacao;

namespace Questao5.Application.Handlers.interfaces
{
    public interface IMovimentacaoHandler
    {
        MovimentoOutput Handle(MovimentoCommand input);
    }
}
