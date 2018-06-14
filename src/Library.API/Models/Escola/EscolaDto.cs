

using System;
using System.Collections.Generic;
using ElevaCase.API.Entities;
using ElevaCase.API.Models.Turma;

namespace ElevaCase.API.Models.Escola
{
    public class EscolaDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public ICollection<TurmaDto> Turmas { get; set; }
            = new List<TurmaDto>();
    }
}
