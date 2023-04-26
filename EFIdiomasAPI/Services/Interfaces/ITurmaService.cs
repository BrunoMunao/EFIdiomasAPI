using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;

namespace EFIdiomasAPI.Services.Interfaces
{
    public interface ITurmaService
    {
        Task<Turma> Create(CreateTurmaDto turmaRequest);
        Task<Turma> Get(string numero);
        Task<IEnumerable<Turma>> GetAll();
        Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero);
        Task<Turma> Delete(string numero);
    }
}
