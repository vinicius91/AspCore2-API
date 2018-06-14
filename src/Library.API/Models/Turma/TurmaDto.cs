using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevaCase.API.Models.Turma
{
    public class TurmaDto
    {

        public Guid Id { get; set; }

        public string NomeCompleto { get; set; }

        public string Etapa { get; set; }

        public int Numero { get; set; }

        public int Ano { get; set; }

        public string EscolaId { get; set; }


    }
}
