﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    [Table("idempotencia")]
    public class IdempotenciaEntity
    {
        [Column("chave_idempotencia")]
        public string ChaveIdempotencia { get; set; }
        [Column("requisicao")]
        public string Requisicao { get; set; }
        [Column("resultado")]
        public string Resultado { get; set; }
        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; }
    }
}
