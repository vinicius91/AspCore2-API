using System;
using System.Collections.Generic;
using ElevaCase.API.Entities;

namespace ElevaCase.API.Services
{
    public interface IElevaRepository
    {
        //Escola
        IEnumerable<Escola> GetEscolas();
        IEnumerable<Escola> GetEscolasLazy();
        Escola GetEscola(Guid escolaId);
        IEnumerable<Escola> GetEscolas(IEnumerable<Guid> authosIds);
        void AddEscola(Escola escola);
        void DeleteEscola(Escola escola);
        void UpdateEscola(Escola escola);
        bool EscolaExists(Guid escolaId);
        //Turma
        IEnumerable<Turma> GetTurmasForEscola(Guid escolaId);
        IEnumerable<Turma> GetAllTurmas();
        IEnumerable<Object> GetTurmasWithEscola();
        Turma GetTurmaForEscola(Guid escolaId, Guid turmaId);
        void AddTurmaForEscola(Guid escolaId, Turma turma);
        void UpdateTurmaForEscola(Turma turma);
        void DeleteTurma(Turma turma);
        //Shared
        bool Save();
    }
}
