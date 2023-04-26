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
            if (!ValidaCPF.ValidarCPF(alunoRequest.CPF) || !ValidaEmail.ValidarEmail(alunoRequest.Email))
            {
                return null;
            }

            var novoAluno = new Aluno
            {
                Nome = alunoRequest.Nome,
                CPF = alunoRequest.CPF,
                Email = alunoRequest.Email
            };

            var numeroTurmas = alunoRequest.NumerosTurmas;

            return await _repository.Create(novoAluno, numeroTurmas);
        }

        public async Task<Aluno> Get(string cpf)
        {
            try
            {
                return await _repository.Get(cpf);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter aluno: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _repository.GetAll();

        }

        public async Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf)
        {
            return await _repository.Update(alunoRequest, cpf);
        }
        public async Task<Aluno> Delete(string cpf)
        {
            return await _repository.Delete(cpf);
        }
    }
}
