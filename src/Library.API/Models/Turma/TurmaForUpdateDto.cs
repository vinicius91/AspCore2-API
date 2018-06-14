
using System;

namespace ElevaCase.API.Models.Turma
{
    public class TurmaForUpdateDto : TurmaForManipulationDto
    {

        public Guid EscolaId { get; set; }
    }
}
