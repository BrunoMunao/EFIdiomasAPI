using EFIdiomasAPI.Data;
using EFIdiomasAPI.Data.DTOs;
using EFIdiomasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Controllers
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
		public async Task<ActionResult<List<Aluno>>> Create(CreateAlunoDto alunoRequest)
		{
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

		[HttpGet("{cpf}")]
		public async Task<ActionResult<List<Aluno>>> Get(string cpf)
		{
			var alunos = await _context.Alunos
				.Where(a => a.CPF == cpf)
				.ToListAsync();

			return alunos;
		}

		[HttpPut("{cpf}")]
		public async Task<ActionResult<List<Aluno>>> Put(UpdateAlunoDto alunoRequest, string cpf)
		{
			Aluno aluno = await _context.Alunos.FindAsync(cpf);
			if (aluno == null)
			{
				return NotFound();
			}

			aluno.Nome = alunoRequest.Nome;
			aluno.Email = alunoRequest.Email;
			

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
