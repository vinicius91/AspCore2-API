
using System;
using System.Collections.Generic;
using AutoMapper;
using ElevaCase.API.Entities;
using ElevaCase.API.Helpers;
using ElevaCase.API.Models.Turma;
using ElevaCase.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElevaCase.API.Controllers
{
    [Route("api/escolas/{escolaId}/turmas")]
    public class TurmasController : Controller
    {

        private IElevaRepository _elevaRepository;
        private ILogger<TurmasController> _logger;

        public TurmasController(IElevaRepository elevaRepository, ILogger<TurmasController> logger)
        {
            _elevaRepository = elevaRepository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetTurmasForEscola(Guid escolaId)
        {
            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmasForEscolaFromRepo = _elevaRepository.GetTurmasForEscola(escolaId);
            var turmasForEscola = Mapper.Map<IEnumerable<TurmaDto>>(turmasForEscolaFromRepo);

            return Ok(turmasForEscola);

        }

        [HttpGet("all")]
        public IActionResult GetAllTurmas()
        {


            var turmasForEscolaFromRepo = _elevaRepository.GetTurmasWithEscola();
            return Ok(turmasForEscolaFromRepo);

        }

        [HttpGet("all/comEscola")]
        public IActionResult GetAllTurmasWithEscola()
        {
            var turmasForEscolaFromRepo = _elevaRepository.GetTurmasWithEscola();
            return Ok(turmasForEscolaFromRepo);

        }

        [HttpGet("{id}", Name = "GetTurmaForEscola")]
        public IActionResult GetTurmaForEscola(Guid escolaId, Guid id)
        {
            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmaForEscolaFromRepo = _elevaRepository.GetTurmaForEscola(escolaId, id);

            if (turmaForEscolaFromRepo == null)
            {
                return NotFound();
            }

            var turmaForEscola = Mapper.Map<TurmaDto>(turmaForEscolaFromRepo);

            return Ok(turmaForEscola);

        }

        [HttpPost()]
        public IActionResult CreateTurmaForEscola(Guid escolaId, [FromBody] TurmaForCreationDto turma)
        {
            if (turma == null)
            {
                return BadRequest();
            }

            if (turma.Etapa != "Ensino Médio" && turma.Etapa != "Ensino Fundamental")
            {
                ModelState.AddModelError(nameof(TurmaForCreationDto), "A etapa não está correta");
            }

            if (!ModelState.IsValid)
            {
                // return 422
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmaEntity = Mapper.Map<Turma>(turma);

            _elevaRepository.AddTurmaForEscola(escolaId, turmaEntity);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Creating a turma for escola {escolaId} failed on save.");
            }

            var turmaToReturn = Mapper.Map<TurmaDto>(turmaEntity);

            return CreatedAtRoute("GetTurmaForEscola", new { escolaId = escolaId, id = turmaToReturn.Id }, turmaToReturn);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurmaForEscola(Guid escolaId, Guid id)
        {
            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmaForEscolaFromRepo = _elevaRepository.GetTurmaForEscola(escolaId, id);
            if (turmaForEscolaFromRepo == null)
            {
                return NotFound();
            }

            _elevaRepository.DeleteTurma(turmaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Deleting turma {id} for escola {escolaId} failed on save");
            }

            _logger.LogInformation(100, $"Turma {id} for escola {escolaId} was deleted");

            return NoContent();

        }


        [HttpPut("{id}")]
        public IActionResult UpdateTurmaForEscola(Guid escolaId, Guid id, [FromBody] TurmaForUpdateDto turma)
        {

            if (turma == null)
            {
                return BadRequest();
            }

            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmaForEscolaFromRepo = _elevaRepository.GetTurmaForEscola(escolaId, id);
            if (turmaForEscolaFromRepo == null)
            {
                var turmaToAdd = Mapper.Map<Turma>(turma);
                turmaToAdd.Id = id;

                _elevaRepository.AddTurmaForEscola(escolaId, turmaToAdd);
                if (!_elevaRepository.Save())
                {
                    throw new Exception("Creating a turma with upserting failed to save.");
                }

                var turmaToReturn = Mapper.Map<TurmaDto>(turmaToAdd);

                return CreatedAtRoute("GetTurmaForEscola", new { escolaId = escolaId, id = turmaToReturn.Id },
                    turmaToReturn);
            }

            Mapper.Map(turma, turmaForEscolaFromRepo);

            _elevaRepository.UpdateTurmaForEscola(turmaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Updating turma {id} for escola {escolaId} failed on save");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateTurmaForEscola(Guid escolaId, Guid id,
            [FromBody] JsonPatchDocument<TurmaForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_elevaRepository.EscolaExists(escolaId))
            {
                return NotFound();
            }

            var turmaForEscolaFromRepo = _elevaRepository.GetTurmaForEscola(escolaId, id);
            if (turmaForEscolaFromRepo == null)
            {
                var turmaDto = new TurmaForUpdateDto();
                patchDoc.ApplyTo(turmaDto, ModelState);

                if (turmaDto.Etapa != "Ensino Médio" && turmaDto.Etapa != "Ensino Fundamental")
                {
                    ModelState.AddModelError(nameof(TurmaForCreationDto), "A etapa não está correta");
                }

                TryValidateModel(turmaDto);

                if (!ModelState.IsValid)
                {
                    return new UnprocessableEntityObjectResult(ModelState);
                }

                var turmaToAdd = Mapper.Map<Turma>(turmaDto);
                turmaToAdd.Id = id;

                _elevaRepository.AddTurmaForEscola(escolaId, turmaToAdd);

                if (!_elevaRepository.Save())
                {
                    throw new Exception("Creating a turma with upserting failed to save.");
                }

                var turmaToReturn = Mapper.Map<TurmaDto>(turmaToAdd);

                return CreatedAtRoute("GetTurmaForEscola", new { escolaId = escolaId, id = turmaToReturn.Id },
                    turmaToReturn);


            }

            var turmaToPatch = Mapper.Map<TurmaForUpdateDto>(turmaForEscolaFromRepo);

            patchDoc.ApplyTo(turmaToPatch, ModelState);


            if (turmaToPatch.Etapa != "Ensino Médio" && turmaToPatch.Etapa != "Ensino Fundamental")
            {
                ModelState.AddModelError(nameof(TurmaForCreationDto), "A etapa não está correta");
            }

            TryValidateModel(turmaToPatch);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            // add validation

            Mapper.Map(turmaToPatch, turmaForEscolaFromRepo);

            _elevaRepository.UpdateTurmaForEscola(turmaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Patching turma {id} for escola {escolaId} failed on save");
            }

            return NoContent();


        }

        [HttpPatch("{id}/novaescola/{newEscolaId}")]
        public IActionResult AlterEscolaForTurma(Guid escolaId, Guid id, Guid newEscolaId)
        {
            if (!_elevaRepository.EscolaExists(escolaId) || !_elevaRepository.EscolaExists(newEscolaId))
            {
                return NotFound();
            }

            var turmaForEscolaFromRepo = _elevaRepository.GetTurmaForEscola(escolaId, id);

            if (turmaForEscolaFromRepo == null)
            {
                return NotFound();
            }

            turmaForEscolaFromRepo.EscolaId = newEscolaId;

            _elevaRepository.UpdateTurmaForEscola(turmaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Patching turma {id} for escola {escolaId} failed on save");
            }

            return NoContent();
        }


    }
}
