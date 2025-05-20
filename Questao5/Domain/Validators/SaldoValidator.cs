using Questao5.Application.Queries.Requests.Saldo;
using Questao5.Domain.Exceptions;
using Questao5.Domain.RepositoryContracts.Query;
using Questao5.Domain.Validators.Interfaces;

namespace Questao5.Domain.Validators
{
    public class SaldoValidator: IValidator
    {
        private readonly SaldoContaQuery _saldo;
        private readonly IReadContaRepository readContaRepository;
        public SaldoValidator(IReadContaRepository readContaRepository) 
        { 
            this.readContaRepository = readContaRepository;
        }

        public void Validate()
        {
            var contas = this.readContaRepository.GetAll();

            var conta = contas.Where(w => w.IdContaCorrente == this._saldo.IdContaCorrente).FirstOrDefault();

            if (conta == null)
            {
                throw new GenericException("Conta inválida", "INVALID_ACCOUNT", System.Net.HttpStatusCode.BadRequest);
            }

            if (!conta.Ativo)
            {
                throw new GenericException("Conta inativa", "INACTIVE_ACCOUNT", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
