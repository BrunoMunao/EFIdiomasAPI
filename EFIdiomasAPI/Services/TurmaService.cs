using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Repository.Interfaces;
using EFIdiomasAPI.Services.Interfaces;

namespace EFIdiomasAPI.Services
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
            // Cria a nova turma
            var novaTurma = new Turma
            {
                Nome = turmaRequest.Nome,
                Numero = turmaRequest.Numero,
                AnoLetivo = turmaRequest.AnoLetivo
            };
            try
            {
                // Cria a nova turma no banco de dados
                return await _repository.Create(novaTurma, turmaRequest.CPFAlunos);
            }
			catch (Exception ex)
			{
				// Se ocorrer um erro ao criar a turma
				throw new Exception($"Erro ao criar turma: {ex.Message}");
			}
		}

        public async Task<Turma> Get(string numero)
        {
            try
            {
                // Retorna turma pelo numero
                return await _repository.Get(numero);
            }
            catch (Exception ex)
            {
                // Erro ao obter a turma
                throw new Exception($"Erro ao obter turma: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Turma>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao obter todas os turmas
                throw new Exception($"Erro ao buscar todas as turmas: {ex.Message}");
            }
		}

        public async Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero)
        {
            try 
            {
				return await _repository.Update(turmaRequest, numero);
			}
            catch (Exception ex)
			{
				// Se ocorrer um erro ao atualizar a turma
				throw new Exception($"Erro ao atualizar a turma: {ex.Message}");
			}
		}

        public async Task<Turma> Delete(string numero)
        {
            try
            {
                return await _repository.Delete(numero);
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao excluir a turma
                throw new Exception($"Erro ao excluir turma: {ex.Message}");
            }
		}
    }
}
