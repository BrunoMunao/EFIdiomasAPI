using EFIdiomasAPI.Application.DTOs;
using EFIdiomasAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFIdiomasAPI.Domain.Interfaces
{
    public interface IAlunoService
	{
		Task<Aluno> Create(CreateAlunoDto alunoRequest);
		Task<Aluno> Get(string cpf);
		Task<IEnumerable<Aluno>> GetAll();
		Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf);
		Task<Aluno> Delete(string cpf);
	}
}
