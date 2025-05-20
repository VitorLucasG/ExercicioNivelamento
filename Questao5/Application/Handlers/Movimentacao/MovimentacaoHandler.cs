using System.Text.Json;
using FluentAssertions;
using Questao5.Application.Commands.Requests.Movimentacao;
using Questao5.Application.Commands.Responses.Movimentacao;
using Questao5.Application.Handlers.interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.RepositoryContracts.Command;
using Questao5.Domain.RepositoryContracts.Query;

namespace Questao5.Application.Handlers.Movimentacao
{

    public class MovimentacaoHandler: IMovimentacaoHandler
    {
        private readonly IWriteMovimentoContaRepository _writeMovimentoRepository;
        private readonly IReadContaRepository _readContaRepository;
        private readonly IWriteIdempotenciaRepository _writeIdempotenciaRepository;
        private readonly IReadIdempotenciaRepository _readIdempotenciaRepository;

        public MovimentacaoHandler(IWriteMovimentoContaRepository movimentoRepository,
                                   IReadContaRepository contaRepository, 
                                   IWriteIdempotenciaRepository idempotenciaRepository,
                                   IReadIdempotenciaRepository readIdempotenciaRepository
                                   ) { 
            this._writeMovimentoRepository = movimentoRepository;
            this._readContaRepository = contaRepository;
            this._writeIdempotenciaRepository = idempotenciaRepository;
            this._readIdempotenciaRepository = readIdempotenciaRepository;
        }

        public MovimentoOutput Handle(MovimentoCommand input)
        {
            var movimento = new MovimentoEntity
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = input.IdContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = input.TipoMovimento,
                Valor = input.Valor
            };

            movimento.Validate(this._readContaRepository);

            var idempotencia = RecuperarIdempotencia(input);

            if (idempotencia != null)
            {
                return JsonSerializer.Deserialize<MovimentoOutput>(idempotencia.Resultado);
            }

            this._writeMovimentoRepository.Add(movimento);

            var movimentos = this._writeMovimentoRepository.GetAll();

            var creditos = movimentos.Where(w => w.TipoMovimento == "C" && w.IdContaCorrente == movimento.IdContaCorrente).Select(s => s.Valor).Sum();
            var debitos = movimentos.Where(w => w.TipoMovimento == "D" && w.IdContaCorrente == movimento.IdContaCorrente).Select(s => s.Valor).Sum();

            var result = new MovimentoOutput();

            result.IdConta = movimento.IdContaCorrente;
            result.Saldo = creditos - debitos;

            InserirIdempotencia(input, result);

            return result;
        }

        private IdempotenciaEntity? RecuperarIdempotencia(MovimentoCommand input)
        {
            var registros = this._readIdempotenciaRepository.GetAll();

            var result = registros.Where(w => JsonSerializer.Deserialize<MovimentoCommand>(w.Requisicao) == input && w.DataCriacao.AddMinutes(5) >= DateTime.Now).FirstOrDefault();

            return result;
        }

        private void InserirIdempotencia(MovimentoCommand input, MovimentoOutput response)
        {
            var idempotencia = new IdempotenciaEntity();

            idempotencia.ChaveIdempotencia = Guid.NewGuid().ToString();
            idempotencia.Requisicao = JsonSerializer.Serialize(input);
            idempotencia.Resultado = JsonSerializer.Serialize(response);
            idempotencia.DataCriacao = DateTime.Now;

            this._writeIdempotenciaRepository.Add(idempotencia);
        }
    }
}
