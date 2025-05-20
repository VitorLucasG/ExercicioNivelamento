using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Globalization;
using Questao5.Domain.RepositoryContracts.Query;
using Questao5.Domain.Validators;

namespace Questao5.Domain.Entities
{
    [Table("movimento")]
    public class MovimentoEntity
    {

        [Column("idmovimento")]
        public string IdMovimento {  get; set; }
        [Column("idcontacorrente")]
        public string IdContaCorrente {  get; set; }
        [Column("datamovimento")]
        public DateTime DataMovimento {  get; set; }
        [Column("tipomovimento")]
        public string TipoMovimento {  get; set; }
        [Column("valor")]
        public decimal Valor {  get; set; }

        public void Validate(IReadContaRepository repository)
        {
            new MovimentoValidator(repository, this).Validate();
        }

    }
}
