using EFIdiomasAPI.Data;
using EFIdiomasAPI.DTOs;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFIdiomasAPI.Repository
{
	public class TurmaRepository : ITurmaRepository
	{

		private readonly DataContext _context;

		// Construtor para injetar o DataContext
		public TurmaRepository(DataContext context)
		{
			_context = context;
		}

		// Método para criar uma turma
		public async Task<Turma> Create(Turma turma, List<string> numeroAlunos)
		{
			// Busca os alunos com os CPFs fornecidos
			var alunos = await _context.Alunos
				.Where(a => numeroAlunos.Contains(a.CPF))
				.ToListAsync();

			// Adiciona as turmas ao aluno e salva no banco de dados
			turma.Alunos = alunos;
			_context.Turmas.Add(turma);
			await _context.SaveChangesAsync();
			return await Get(turma.Numero);
		}

		// Método para buscar uma turma pelo seu número
		public async Task<Turma> Get(string numero)
		{
			// Busca a turma com o número fornecido
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);

			// Verifica se a turma foi encontrada
			if (turma == null)
			{
				throw new Exception($"Turma com número {numero} não encontrado");
			}

			return turma;
		}

		// Método para buscar todas as turmas
		public async Task<IEnumerable<Turma>> GetAll()
		{
			// Busca todas as turmas e seus alunos, selecionando apenas os campos necessários
			var turmas = await _context.Turmas.Include(t => t.Alunos).Select(t => new Turma
			{
				Nome = t.Nome,
				Numero = t.Numero,
				AnoLetivo = t.AnoLetivo,
				Alunos = t.Alunos.Select(a => new Aluno
				{
					Nome = a.Nome,
					CPF = a.CPF,
					Email = a.Email,
				}).ToList()
			}).ToListAsync();

			// Verifica se tem alguma turma
			if (turmas == null || turmas.Count == 0)
			{
				throw new Exception($"Não foi encontrada nenhuma turma.");
			}

			return turmas;
		}

		public async Task<Turma> Update(UpdateTurmaDto turmaRequest, string numero)
		{
			// Busca a turma a ser atualizada
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);
			if (turma == null)
			{
				throw new ArgumentException($"Turma com número {numero} não encontrada", nameof(numero));
			}

			var alunos =
				await _context.Alunos
				.Where(a => turmaRequest.CPFAlunos.Contains(a.CPF))
				.ToListAsync();

			turma.Nome = turmaRequest.Nome;
			turma.AnoLetivo = turmaRequest.AnoLetivo;
			turma.Alunos = alunos;

			// Salva as alteraçoes feitas retornando a turma atualizada
			await _context.SaveChangesAsync();
			return await Get(turma.Numero);
		}

		// Método para excluir a turma
		public async Task<Turma> Delete(string numero)
		{
			var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Numero == numero);

			// Verifica se a turma existe
			if (turma == null)
			{
				throw new ArgumentException($"Turma com número {numero} não encontrada", nameof(numero));
			}

			// Restrição que impede uma turma ser excluída se tiver alunos
			if (turma.Alunos.Any())
			{
				throw new Exception("Não é possível excluir uma turma que possui alunos");
			}
			
			// Remove a turma
			_context.Turmas.Remove(turma);
			await _context.SaveChangesAsync();

			return turma;
		}

	}
}
