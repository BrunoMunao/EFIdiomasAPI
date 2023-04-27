using EFIdiomasAPI.Data;
using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Repository
{
	public class TurmaRepository : ITurmaRepository
	{

		private readonly DataContext _context;

		public TurmaRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Turma> Create(Turma turma, List<string> numeroAlunos)
		{
			var alunos = await _context.Alunos
				.Where(a => numeroAlunos.Contains(a.CPF))
				.ToListAsync();

			turma.Alunos = alunos;

			_context.Turmas.Add(turma);
			await _context.SaveChangesAsync();
			return await Get(turma.Numero);
		}

		public async Task<Turma> Get(string numero)
		{
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);

			if (turma == null)
			{
				throw new Exception($"Turma com número {numero} não encontrado");
			}

			return turma;
		}

		public async Task<IEnumerable<Turma>> GetAll()
		{
			var turmas = await _context.Turmas.Include(t => t.Alunos).Select(t => new Turma
			{
				Nome = t.Nome,
				Numero = t.Numero,
				AnoLetivo = t.AnoLetivo,
				Alunos = t.Alunos.Select(a => new Aluno
				{
					Nome = a.Nome,
					CPF = a.CPF,
					Email = a.Email,
				}).ToList()
			}).ToListAsync();

			return turmas;
		}

		public async Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero)
		{
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);
			if (turma == null)
			{
				return null;
			}

			var alunos =
				await _context.Alunos
				.Where(a => turmaRequest.CPFAlunos.Contains(a.CPF))
				.ToListAsync();

			turma.Nome = turmaRequest.Nome;
			turma.AnoLetivo = turmaRequest.AnoLetivo;
			turma.Alunos = alunos;

			await _context.SaveChangesAsync();
			return await Get(turma.Numero);
		}

		public async Task<Turma> Delete(string numero)
		{
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);

			if (turma == null)
			{
				return null;
			}

			if (turma.Alunos.Any())
			{
				throw new Exception("Não é possível excluir uma turma que possui alunos");
			}

			_context.Turmas.Remove(turma);
			await _context.SaveChangesAsync();

			return turma;
		}

	}
}
