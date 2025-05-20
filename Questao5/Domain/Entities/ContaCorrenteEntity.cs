using System.ComponentModel.DataAnnotations.Schema;
using FluentAssertions.Equivalency;

namespace Questao5.Domain.Entities
{
    [Table("contacorrente")]
    public class ContaCorrenteEntity
    {
        [Column("idcontacorrente")]
        public string IdContaCorrente { get; set; }
        [Column("numero")]
        public int Numero {  get; set; }
        [Column("nome")]
        public string Nome {  get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; }
    }
}
