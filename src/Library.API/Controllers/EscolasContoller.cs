

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ElevaCase.API.Entities;
using ElevaCase.API.Helpers;
using ElevaCase.API.Models;
using ElevaCase.API.Models.Escola;
using ElevaCase.API.Models.Turma;
using ElevaCase.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ElevaCase.API.Controllers
{
    [Route("api/escolas")]
    public class EscolasContoller : Controller
    {
        private IElevaRepository _elevaRepository;

        public EscolasContoller(IElevaRepository elevaRepository)
        {
            _elevaRepository = elevaRepository;
        }

        [HttpGet()]
        public IActionResult GetEscolas()
        {
            var escolasFromRepo = _elevaRepository.GetEscolas();
            var escolas = Mapper.Map<IEnumerable<EscolaDto>>(escolasFromRepo);
            return Ok(escolas);
        }

        [HttpGet("turmasCount")]
        public IActionResult GetEscolasTurmasCount()
        {
            var escolasFromRepo = _elevaRepository.GetEscolasLazy();
            var escolas = escolasFromRepo.Select(e => new
            {
                Id = e.Id,
                Nome = e.Nome,
                NumeroTurmas = e.Turmas.Count()

            });
            return Ok(escolas);
        }

        [HttpGet("{id}", Name = "GetEscola")]
        public IActionResult GetEscola(Guid id)
        {

            var escolaFromRepo = _elevaRepository.GetEscola(id);

            if (escolaFromRepo == null)
            {
                return NotFound();
            }

            var escola = Mapper.Map<EscolaDto>(escolaFromRepo);
            return Ok(escola);
        }

        [HttpPost()]
        public IActionResult CreateEscola([FromBody] EscolaForCreationDto escola)
        {
            if (escola == null)
            {
                return BadRequest();
            }

            var escolaEntity = Mapper.Map<Escola>(escola);

            _elevaRepository.AddEscola(escolaEntity);

            if (!_elevaRepository.Save())
            {
                throw new Exception("Criar uma escola falhou ao salvar");
            }

            var escolaToReturn = Mapper.Map<EscolaDto>(escolaEntity);

            return CreatedAtRoute("GetEscola", new { id = escolaToReturn.Id }, escolaToReturn);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTurmaForEscola(Guid id, [FromBody] EscolaForUpdateDto escola)
        {

            if (escola == null)
            {
                return BadRequest();
            }

            var escolaForEscolaFromRepo = _elevaRepository.GetEscola(id);

            if (escolaForEscolaFromRepo == null)
            {
                var escolaToAdd = Mapper.Map<Escola>(escola);
                escolaToAdd.Id = id;

                _elevaRepository.AddEscola(escolaToAdd);
                if (!_elevaRepository.Save())
                {
                    throw new Exception("Creating a escola with upserting failed to save.");
                }

                var escolaToReturn = Mapper.Map<EscolaDto>(escolaToAdd);

                return CreatedAtRoute("GetTurmaForEscola", new {id = id },
                    escolaToReturn);
            }

            Mapper.Map(escola, escolaForEscolaFromRepo);

            _elevaRepository.UpdateEscola(escolaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Updating escola {id} failed on save");
            }

            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult PartialUpdateEscola(Guid id,
            [FromBody] JsonPatchDocument<EscolaForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }


            var escolaForEscolaFromRepo = _elevaRepository.GetEscola(id);
            if (escolaForEscolaFromRepo == null)
            {
                var escolaDto = new EscolaForUpdateDto();
                patchDoc.ApplyTo(escolaDto, ModelState);

                TryValidateModel(escolaDto);

                if (!ModelState.IsValid)
                {
                    return new UnprocessableEntityObjectResult(ModelState);
                }

                var escolaToAdd = Mapper.Map<Escola>(escolaDto);
                escolaToAdd.Id = id;

                _elevaRepository.AddEscola(escolaToAdd);

                if (!_elevaRepository.Save())
                {
                    throw new Exception("Creating a escola with upserting failed to save.");
                }

                var escolaToReturn = Mapper.Map<TurmaDto>(escolaToAdd);

                return CreatedAtRoute("GetEscola", new { id = escolaToReturn.Id },
                    escolaToReturn);


            }

            var escolaToPatch = Mapper.Map<EscolaForUpdateDto>(escolaForEscolaFromRepo);

            patchDoc.ApplyTo(escolaToPatch, ModelState);

            TryValidateModel(escolaToPatch);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            // add validation

            Mapper.Map(escolaToPatch, escolaForEscolaFromRepo);

            _elevaRepository.UpdateEscola(escolaForEscolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Patching escola {id} failed on save");
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEscola(Guid id)
        {
            if (!_elevaRepository.EscolaExists(id))
            {
                return NotFound();
            }

            var escolaFromRepo = _elevaRepository.GetEscola(id);
            if (escolaFromRepo == null)
            {
                return NotFound();
            }

            _elevaRepository.DeleteEscola(escolaFromRepo);

            if (!_elevaRepository.Save())
            {
                throw new Exception($"Deleting escola {id} failed on save");
            }


            return NoContent();

        }

        [HttpPost("{id}")]
        public IActionResult BlockEscolaCreation(Guid id)
        {
            if (_elevaRepository.EscolaExists(id))
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }
    }
}
