using EFIdiomasAPI.Data;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EFIdiomasAPI.Repository
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

			if (numeroTurmas == null || numeroTurmas.Count == 0 || turmas.IsNullOrEmpty()) // Restrição 1: Aluno deve ser cadastrado com no mínimo 1 turma;
			{
				throw new Exception("O aluno deve ser associado a pelo menos uma turma");
			}

			if (turmas.Count > 5) // Restrição 4: uma turma não pode ter mais de 5 alunos
			{
				throw new Exception($"O número máximo de alunos na turma permitido é 5");
			}

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

			if (turmas.Count > 5) // Restrição 4: uma turma não pode ter mais de 5 alunos
			{
				throw new Exception($"O número máximo de turmas permitido é 5");
			}

			aluno.Nome = alunoRequest.Nome;
			aluno.Email = alunoRequest.Email;
			aluno.Turmas = turmas;

			await _context.SaveChangesAsync();
			return await Get(aluno.CPF);
		}

		public async Task<Aluno> Delete(string cpf)
		{
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);

			if (aluno == null)
			{
				return null;
			}

			if (aluno.Turmas.Count > 0) // Restrição 3: um aluno não pode ser excluído se estiver matriculado em alguma turma
			{
				throw new Exception($"O aluno {aluno.Nome} está matriculado em {aluno.Turmas.Count} turmas e não pode ser excluído.");
			}

			_context.Alunos.Remove(aluno);
			await _context.SaveChangesAsync();

			return aluno;
		}


	}
}
