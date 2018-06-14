using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElevaCase.API.Models.Turma
{
    public class TurmaForManipulationDto
    {
        [Required]
        [MaxLength(50)]
        public string Etapa { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int Ano { get; set; }

    }
}
