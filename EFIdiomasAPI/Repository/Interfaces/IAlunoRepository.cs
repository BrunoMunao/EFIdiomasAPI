using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;

namespace EFIdiomasAPI.Repository.Interfaces
{
    public interface IAlunoRepository
    {
        Task<Aluno> Create(Aluno aluno, List<string> numeroTurmas);
        Task<Aluno> Get(string cpf);
        Task<IEnumerable<Aluno>> GetAll();
        Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf);
        Task<Aluno> Delete(string cpf);
    }
}
