using EFIdiomasAPI.Infrastructure.Data;
using EFIdiomasAPI.Application.DTOs;
using EFIdiomasAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Validadores;

namespace EFIdiomasAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;

        public AlunoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Create(CreateAlunoDto alunoRequest)
        {

            if (!ValidaCPF.ValidarCPF(alunoRequest.CPF) || !ValidaEmail.ValidarEmail(alunoRequest.Email))
            {
                return BadRequest();
            }
            var novoAluno = new Aluno
            {
                Nome = alunoRequest.Nome,
                CPF = alunoRequest.CPF,
                Email = alunoRequest.Email
            };

            var turmas =
                await _context.Turmas
                .Where(t => alunoRequest.NumerosTurmas.Contains(t.Numero))
                .ToListAsync();


            novoAluno.Turmas = turmas;

            _context.Alunos.Add(novoAluno);
            await _context.SaveChangesAsync();

            return await Get(novoAluno.CPF);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAll()
        {
            var alunos = await _context.Alunos.Include(a => a.Turmas).Select(a => new Aluno
            {
                Nome = a.Nome,
                CPF = a.CPF,
                Email = a.Email,
                Turmas = a.Turmas.Select(t => new Turma
                {
                    Nome = t.Nome,
                    Numero = t.Numero,
                    AnoLetivo = t.AnoLetivo,
                }).ToList()
            }).ToListAsync();

            return Ok(alunos);
        }


        [HttpGet("{cpf}")]
        public async Task<ActionResult<Aluno>> Get(string cpf)
        {
            var aluno = _context.Alunos.Include(a => a.Turmas).FirstOrDefault(a => a.CPF == cpf);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPut("{cpf}")]
        public async Task<ActionResult<Aluno>> Put(UpdateAlunoDto alunoRequest, string cpf)
        {
            Aluno aluno = _context.Alunos.Include(a => a.Turmas).FirstOrDefault(a => a.CPF == cpf);
            if (aluno == null)
            {
                return NotFound();
            }

            var turmas =
                await _context.Turmas
                .Where(t => alunoRequest.NumerosTurmas.Contains(t.Numero))
                .ToListAsync();

            aluno.Nome = alunoRequest.Nome;
            aluno.Email = alunoRequest.Email;
            aluno.Turmas = turmas;

            await _context.SaveChangesAsync();
            return await Get(aluno.CPF);
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> Delete(string cpf)
        {
            Aluno aluno = _context.Alunos.FirstOrDefault(a => a.CPF == cpf);
            if (aluno == null)
            {
                return NotFound();
            }
            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
