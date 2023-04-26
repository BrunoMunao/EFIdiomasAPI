using EFIdiomasAPI.Application.DTOs;
using EFIdiomasAPI.Domain.Entities;
using EFIdiomasAPI.Domain.Interfaces;
using EFIdiomasAPI.Infrastructure.Interfaces;

namespace EFIdiomasAPI.Domain.Services
{
	public class TurmaService : ITurmaService
	{

		private readonly ITurmaRepository _repository;

        public TurmaService(ITurmaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Turma> Create(CreateTurmaDto turmaRequest)
		{
			var novaTurma = new Turma
			{
				Nome = turmaRequest.Nome,
				Numero = turmaRequest.Numero,
				AnoLetivo = turmaRequest.AnoLetivo
			};

			var numeroAlunos = turmaRequest.CPFAlunos;

			return await _repository.Create(novaTurma, numeroAlunos);
		}

		public async Task<Turma> Get(string numero)
		{
			try
			{
				return await _repository.Get(numero);
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro ao obter turma: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Turma>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero)
		{
			return await _repository.Update(turmaRequest, numero);
		}

		public async Task<Turma> Delete(string numero)
		{
			return await _repository.Delete(numero);
		}
	}
}
