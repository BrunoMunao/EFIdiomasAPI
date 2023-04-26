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

		public Task<Aluno> Get(string cpf)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Aluno>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf)
		{
			throw new NotImplementedException();
		}

		public Task<Aluno> Delete(string cpf)
		{
			throw new NotImplementedException();
		}
	}
}
