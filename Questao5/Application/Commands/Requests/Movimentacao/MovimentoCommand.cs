
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Requests.Movimentacao
{
    public class MovimentoCommand
    {
        public MovimentoCommand(string IdContaCorrente, DateTime DataMovimento, string TipoMovimento, decimal Valor)
        {
            this.IdContaCorrente = IdContaCorrente;
            this.DataMovimento = DataMovimento;
            this.TipoMovimento = TipoMovimento;
            this.Valor = Valor;
        }

        public string IdContaCorrente { get; set; }
        [JsonIgnore]
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public decimal Valor { get; set; }


        public static bool operator !=(MovimentoCommand input1, MovimentoCommand input2)
        {
            return !input1.Equals(input2);
        }

        public static bool operator ==(MovimentoCommand input1, MovimentoCommand input2) 
        {
            return input1.Equals(input2);
        }

        public override bool Equals(object Obj)
        {
            MovimentoCommand other = (MovimentoCommand)Obj;
            return (this.IdContaCorrente == other.IdContaCorrente &&
                this.TipoMovimento == other.TipoMovimento &&
                this.Valor == other.Valor);
        }
    }
}
