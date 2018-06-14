

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElevaCase.API.Entities
{
    public class Turma
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Etapa { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int Ano { get; set; }

        [ForeignKey("EscolaId")]
        public Escola Escola { get; set; }

        public Guid EscolaId { get; set; }


    }
}
