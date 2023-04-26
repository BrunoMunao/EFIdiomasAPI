using EFIdiomasAPI.Application.DTOs;
using EFIdiomasAPI.Domain.Entities;
using EFIdiomasAPI.Infrastructure.Data;
using EFIdiomasAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Infrastructure.Repository
{
	public class AlunoRepository : IAlunoRepository
	{
		private readonly DataContext _context;
		public AlunoRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<Aluno> Create(Aluno aluno, List<string> numeroTurmas)
		{

			var turmas = await _context.Turmas
				.Where(t => numeroTurmas.Contains(t.Numero))
				.ToListAsync();

			aluno.Turmas = turmas;

			_context.Alunos.Add(aluno);
			await _context.SaveChangesAsync();
			return await Get(aluno.CPF);
		}

		public async Task<Aluno> Get(string cpf)
		{
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);

			if (aluno == null)
			{
				throw new Exception($"Aluno com CPF {cpf} não encontrado");
			}

			return aluno;
		}

		public async Task<IEnumerable<Aluno>> GetAll()
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

			return alunos;
		}


		public async Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf)
		{
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);
			if (aluno == null)
			{
				return null;
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

		public async Task<Aluno> Delete(string cpf)
		{
			var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.CPF == cpf);

			if (aluno == null)
			{
				return null;
			}

			_context.Alunos.Remove(aluno);
			await _context.SaveChangesAsync();

			return aluno;
		}

	}
}
