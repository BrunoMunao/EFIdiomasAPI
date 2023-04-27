using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Repository.Interfaces;
using EFIdiomasAPI.Services.Interfaces;
using Validadores;

namespace EFIdiomasAPI.Services
{
	public class AlunoService : IAlunoService
	{
		private readonly IAlunoRepository _repository;

		public AlunoService(IAlunoRepository repository)
		{
			_repository = repository;
		}

		public async Task<Aluno> Create(CreateAlunoDto alunoRequest)
		{
			// Verifica se o CPF e e-mail são válidos
			if (!ValidaCPF.ValidarCPF(alunoRequest.CPF) || !ValidaEmail.ValidarEmail(alunoRequest.Email))
			{
				throw new ArgumentException("CPF ou e-mail inválidos.");
			}

			// Verifica se o aluno está associado a pelo menos uma turma
			if (alunoRequest.NumerosTurmas == null || !alunoRequest.NumerosTurmas.Any())
			{
				throw new ArgumentException("O aluno deve ser associado a pelo menos uma turma.");
			}

			// Cria o novo aluno
			var novoAluno = new Aluno
			{
				Nome = alunoRequest.Nome,
				CPF = alunoRequest.CPF,
				Email = alunoRequest.Email
			};

			try
			{
				// Cria o aluno no banco de dados
				return await _repository.Create(novoAluno, alunoRequest.NumerosTurmas);
			}
			catch (Exception ex)
			{
				// Se ocorrer um erro ao criar o aluno
				throw new Exception($"Erro ao criar aluno: {ex.Message}");
			}
		}


		public async Task<Aluno> Get(string cpf)
        {
            try
            {
				// Retorna o aluno pelo cpf
                return await _repository.Get(cpf);
            }
            catch (Exception ex)
            {
				// Se ocorrer um erro ao obter o aluno
				throw new Exception($"Erro ao obter aluno: {ex.Message}");
            }
        }

		public async Task<IEnumerable<Aluno>> GetAll()
		{
			try
			{
				// Retorna todos os alunos
				return await _repository.GetAll();
			}
			catch (Exception ex)
			{
				// Se ocorrer um erro ao obter todos os alunos
				throw new Exception($"Erro ao buscar todos os alunos: {ex.Message}");
			}
		}

		public async Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf)
		{
			try
			{
				// Retorna o aluno com as infos atualizadas
				return await _repository.Update(alunoRequest, cpf);
			}
			catch (Exception ex)
			{
				// Se ocorrer um erro ao obter todos os alunos
				throw new Exception($"Erro ao atualizar o aluno: {ex.Message}");
			}
		}

		public async Task<Aluno> Delete(string cpf)
		{
			try
			{
				// Retorna o resultado do aluno a ser excluído
				return await _repository.Delete(cpf);
			}
			catch (Exception ex)
			{
				// Se ocorrer um erro ao excluir o aluno
				throw new Exception($"Erro ao excluir o aluno: {ex.Message}");
			}
		}
	}
}
