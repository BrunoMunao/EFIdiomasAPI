using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;

namespace EFIdiomasAPI.Repository.Interfaces
{
    public interface ITurmaRepository
    {
        Task<Turma> Create(Turma turma, List<string> numeroAlunos);
        Task<Turma> Get(string numero);
        Task<IEnumerable<Turma>> GetAll();
        Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero);
        Task<Turma> Delete(string numero);
    }
}
