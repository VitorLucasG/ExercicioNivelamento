using Questao5.Application.Queries.Requests.Saldo;
using Questao5.Application.Queries.Responses.Saldo;

namespace Questao5.Application.Handlers.interfaces
{
    public interface ISaldoHandler
    {
        SaldoContaQueryOutput Handle(SaldoContaQuery input);
    }
}
