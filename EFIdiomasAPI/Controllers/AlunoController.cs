using EFIdiomasAPI.Data;
using EFIdiomasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlunoController
	{
		private readonly DataContext _context;

        public AlunoController(DataContext context)
        {
			_context = context;
        }

		[HttpGet]
		public async Task<ActionResult<List<Aluno>>> Get(int alunoId)
		{
			var alunos = await _context.Alunos
				.Where(a => a.Id == alunoId)
				.ToListAsync();

			return alunos;
		}

		[HttpPost]
		public async Task<ActionResult<List<Aluno>>> Create(Aluno aluno)
		{
			_context.Alunos.Add(aluno);
			await _context.SaveChangesAsync();

			return await Get(aluno.Id);
		}

	}
}
