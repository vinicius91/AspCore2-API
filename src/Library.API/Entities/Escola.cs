
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElevaCase.API.Entities
{
    public class Escola
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        public ICollection<Turma> Turmas { get; set; }
        = new List<Turma>();


    }
}
