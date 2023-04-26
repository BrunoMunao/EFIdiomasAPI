﻿using EFIdiomasAPI.Infrastructure.Data;
using EFIdiomasAPI.Application.DTOs;
using EFIdiomasAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFIdiomasAPI.Domain.Interfaces;

namespace EFIdiomasAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly DataContext _context;

        public AlunoController(IAlunoService alunoService, DataContext context)
        {
            _context = context;
            _alunoService = alunoService;
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Create(CreateAlunoDto alunoRequest)
        {
            var novoAluno = await _alunoService.Create(alunoRequest);
            if (novoAluno == null)
            {
                return BadRequest();
            }

            return novoAluno;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAll()
        {
            var alunos = await _alunoService.GetAll();

            return Ok(alunos);
        }


        [HttpGet("{cpf}")]
        public async Task<ActionResult<Aluno>> Get(string cpf)
        {
			var aluno = await _alunoService.Get(cpf);

			if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPut("{cpf}")]
        public async Task<ActionResult<Aluno>> Put(UpdateAlunoDto alunoRequest, string cpf)
        {
			var aluno = await _alunoService.Update(alunoRequest, cpf);
			if (aluno == null)
			{
				return BadRequest();
			}

			return aluno;
		}

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> Delete(string cpf)
        {
            var aluno = await _alunoService.Delete(cpf);
            if (aluno == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
