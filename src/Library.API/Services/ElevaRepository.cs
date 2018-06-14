using System;
using System.Collections.Generic;
using System.Linq;
using ElevaCase.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElevaCase.API.Services
{
    public class ElevaRepository : IElevaRepository
    {
        private ElevaContext _context;

        public ElevaRepository(ElevaContext context)
        {
            _context = context;
        }

        
        //Escola

        public void AddEscola(Escola escola)
        {
            escola.Id = Guid.NewGuid();
            _context.Escolas.Add(escola);

            // the repository fills the id (instead of using identity columns)
            if (escola.Turmas.Any())
            {
                foreach (var turma in escola.Turmas)
                {
                    turma.Id = Guid.NewGuid();
                }
            }
        }

        public bool EscolaExists(Guid escolaId)
        {
            return _context.Escolas.Any(e => e.Id == escolaId);
        }

        public void DeleteEscola(Escola escola)
        {
            _context.Escolas.Remove(escola);
        }


        public Escola GetEscola(Guid escolaId)
        {
            return _context.Escolas.Include(e => e.Turmas).FirstOrDefault(e => e.Id == escolaId);
        }

        public IEnumerable<Escola> GetEscolas()
        {
            return _context.Escolas.OrderBy(e => e.Nome);
        }

        public IEnumerable<Escola> GetEscolas(IEnumerable<Guid> escolasIds)
        {
            return _context.Escolas.Where(e => escolasIds.Contains(e.Id))
                .OrderBy(e => e.Nome)
                .ToList();
        }

        public IEnumerable<Escola> GetEscolasLazy()
        {
            return _context.Escolas.Include(e => e.Turmas).OrderBy(e => e.Nome);
        }


        public IEnumerable<Object> GetTurmasWithEscola()
        {
            var turmas = _context.Turmas.Include(t => t.Escola).OrderBy(t => t.Etapa).ThenBy(t => t.Ano).ThenBy(t => t.Numero).ToList().Select(t => new
            {
                Id = t.Id,
                NomeCompleto = $"{t.Ano}º do {t.Etapa} - {t.Numero}",
                Ano = t.Ano,
                Etapa = t.Etapa,
                Numero = t.Numero,
                Escola = t.Escola.Nome,
                EscolaId = t.EscolaId

            });

            return turmas;
        }



        public void UpdateEscola(Escola escola)
        {
            // no code in this implementation
        }

        //Turmas

        public Turma GetTurmaForEscola(Guid escolaId, Guid turmaId)
        {
            return _context.Turmas
                .Where(t => t.EscolaId == escolaId && t.Id == turmaId).FirstOrDefault();
        }

        public IEnumerable<Turma> GetTurmasForEscola(Guid escolaId)
        {
            return _context.Turmas
                .Where(t => t.EscolaId == escolaId).OrderBy(t => t.Etapa).ThenBy(t => t.Ano).ThenBy(t => t.Numero).ToList();
        }

        public IEnumerable<Turma> GetAllTurmas()
        {
            return _context.Turmas.OrderBy(t => t.Etapa).ThenBy(t => t.Ano).ThenBy(t => t.Numero).ToList();
        }

        public void UpdateTurmaForEscola(Turma turma)
        {
            // no code in this implementation
        }

        public void DeleteTurma(Turma turma)
        {
            _context.Turmas.Remove(turma);
        }

        public void AddTurmaForEscola(Guid escolaId, Turma turma)
        {
            var escola = GetEscola(escolaId);
            if (escola != null)
            {
                // if there isn't an id filled out (ie: we're not upserting),
                // we should generate one
                if (turma.Id == Guid.Empty)
                {
                    turma.Id = Guid.NewGuid();
                }
                escola.Turmas.Add(turma);
            }
        }


        //Shared

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
