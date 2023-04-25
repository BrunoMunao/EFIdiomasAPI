using EFIdiomasAPI.Data;
using EFIdiomasAPI.Data.DTOs;
using EFIdiomasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TurmaController : ControllerBase
	{
		private readonly DataContext _context;

        public TurmaController(DataContext context)
        {
			_context = context;
        }

		[HttpPost]
		public async Task<ActionResult<List<Turma>>> Create(CreateTurmaDto turmaRequest)
		{
			var novaTurma = new Turma
			{
				Numero = turmaRequest.Numero,
				Nome = turmaRequest.Nome,
				AnoLetivo = turmaRequest.AnoLetivo
			};

			var alunos =
				await _context.Alunos
				.Where(a => turmaRequest.CPFAlunos.Contains(a.CPF))
				.ToListAsync();


			novaTurma.Alunos = alunos;

			_context.Turmas.Add(novaTurma);
			await _context.SaveChangesAsync();

			return await Get(novaTurma.Numero);
		}

		[HttpGet("{numero}")]
		public async Task<ActionResult<List<Turma>>> Get(string numero)
		{
			var turmas = await _context.Turmas
				.Where(t => t.Numero == numero)
				.ToListAsync();

			return turmas;
		}

		[HttpPut("{numero}")]
		public async Task<ActionResult<List<Turma>>> Put(UpdateTurmaDto turmaRequest, string numero)
		{
			Turma turma = await _context.Turmas.FindAsync(numero);
			if (turma == null)
			{
				return NotFound();
			}

			turma.Nome = turmaRequest.Nome;
			turma.AnoLetivo = turmaRequest.AnoLetivo;


			await _context.SaveChangesAsync();
			return await Get(turma.Numero);
		}

		[HttpDelete("{numero}")]
		public async Task<ActionResult> Delete(string numero)
		{
			Turma turma = _context.Turmas.FirstOrDefault(a => a.Numero == numero);
			if (turma == null)
			{
				return NotFound();
			}
			_context.Turmas.Remove(turma);
			_context.SaveChanges();
			return NoContent();
		}
	}

}
