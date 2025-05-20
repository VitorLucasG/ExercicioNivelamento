using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;
using Questao5.Domain.RepositoryContracts.Query;
using Questao5.Domain.Validators.Interfaces;

namespace Questao5.Domain.Validators
{
    public class MovimentoValidator : IValidator
    {
        private readonly MovimentoEntity _movimento;
        private readonly IReadContaRepository readContaRepository;
        public MovimentoValidator(IReadContaRepository contaRepository, MovimentoEntity movimento) 
        { 
            this._movimento = movimento;
            this.readContaRepository = contaRepository;
        }

        public void Validate()
        {
            var movimentosPermitidos = new List<string>
            {
                "C",
                "D"
            };

            var contas = this.readContaRepository.GetAll();

            var conta = contas.Where(w => w.IdContaCorrente == this._movimento.IdContaCorrente).FirstOrDefault();

            if (conta == null)
            {
                throw new GenericException("INVALID_ACCOUNT", "Conta inválida", System.Net.HttpStatusCode.BadRequest);
            }

            if(!conta.Ativo)
            {
                throw new GenericException("INACTIVE_ACCOUNT", "Conta inativa", System.Net.HttpStatusCode.BadRequest);
            }

            if(this._movimento.Valor <= 0)
            {
                throw new GenericException("INVALID_VALUE", "Valor inválido", System.Net.HttpStatusCode.BadRequest);
            }

            if (!movimentosPermitidos.Contains(this._movimento.TipoMovimento))
            {
                throw new GenericException("INVALID_TYPE", "Tipo de operação inválido", System.Net.HttpStatusCode.BadRequest);
            }

        }
    }
}
