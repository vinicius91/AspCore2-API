using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElevaCase.API.Models.Escola
{
    public class EscolaForManipulationDto
    {

        [Required(ErrorMessage = "O nome é necessário")]
        [MaxLength(100, ErrorMessage = "O nome da escola deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

    }
}
