using Questao5.Application.Handlers.interfaces;
using Questao5.Application.Queries.Requests.Saldo;
using Questao5.Application.Queries.Responses.Saldo;
using Questao5.Domain.RepositoryContracts.Query;

namespace Questao5.Application.Handlers.Saldo
{
    public class SaldoHandler: ISaldoHandler
    {

        private readonly IReadMovimentoRepository _readMovimentos;
        public SaldoHandler(IReadMovimentoRepository movimentoRepository) 
        { 
            this._readMovimentos = movimentoRepository;
        }

        public SaldoContaQueryOutput Handle(SaldoContaQuery input)
        {
            var movimentos = this._readMovimentos.GetAll();

            var result = new SaldoContaQueryOutput();

            var creditos = movimentos.Where(w => w.TipoMovimento == "C" && w.IdContaCorrente == input.IdContaCorrente).Select(s => s.Valor).Sum();
            var debitos = movimentos.Where(w => w.TipoMovimento == "D" && w.IdContaCorrente == input.IdContaCorrente).Select(s => s.Valor).Sum();

            result.IdConta = input.IdContaCorrente;
            result.Saldo = creditos - debitos;

            return result;
        }
    }
}
