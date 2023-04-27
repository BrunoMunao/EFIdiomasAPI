using EFIdiomasAPI.Data;
using EFIdiomasAPI.Entities;
using EFIdiomasAPI.Repository.Interfaces;
using EFIdiomasAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Repository
{
	public class AlunoRepository : IAlunoRepository
	{
		private readonly DataContext _context;

		// Construtor para injetar o DataContext
		public AlunoRepository(DataContext context)
		{
			_context = context;
		}

		// Método para criar um aluno
		public async Task<Aluno> Create(Aluno aluno, List<string> numeroTurmas)
		{
			// Busca as turmas com os números fornecidos
			var turmas = await _context.Turmas
				.Where(t => numeroTurmas.Contains(t.Numero))
				.Include(t => t.Alunos)
				.ToListAsync();

			// Verifica se as turmas foram encontradas
			if (turmas == null || turmas.Count == 0)
			{
				throw new Exception("Não foram encontradas turmas com os números fornecidos");
			}

			// Verifica se cada turma tem menos de 5 alunos
			foreach (var turma in turmas) // Restrição 4: uma turma não pode ter mais de 5 alunos
			{
				Console.WriteLine($"Turma {turma.Numero} tem {turma.Alunos.Count} alunos.");
				if (turma.Alunos.Count >= 5)
				{
					throw new Exception($"O número máximo de alunos na turma permitido é 5.");
				}
			}

			// Adiciona as turmas ao aluno e salva no banco de dados
			aluno.Turmas = turmas;
			_context.Alunos.Add(aluno);
			await _context.SaveChangesAsync();
			return await Get(aluno.CPF);
		}

		// Método para buscar um aluno pelo CPF
		public async Task<Aluno> Get(string cpf)
		{
			// Busca o aluno com o CPF fornecido
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);

			// Verifica se o aluno foi encontrado
			if (aluno == null)
			{
				throw new Exception($"Aluno com CPF {cpf} não encontrado");
			}

			return aluno;
		}

		// Método para buscar todos os alunos
		public async Task<IEnumerable<Aluno>> GetAll()
		{
			// Busca todos os alunos e suas turmas, selecionando apenas os campos necessários
			var alunos = await _context.Alunos.Include(a => a.Turmas).Select(a => new Aluno
			{
				Nome = a.Nome,
				CPF = a.CPF,
				Email = a.Email,
				Turmas = a.Turmas.Select(t => new Turma
				{
					Nome = t.Nome,
					Numero = t.Numero,
					AnoLetivo = t.AnoLetivo,
				}).ToList()
			}).ToListAsync();

			// Verifica se tem algum aluno
			if (alunos == null || alunos.Count == 0)
			{
				throw new Exception($"Nenhum aluno encontrado");
			}

			return alunos;
		}

		// Método para atualizar um aluno
		public async Task<Aluno> Update(UpdateAlunoDto alunoRequest, string cpf)
		{
			// Busca o aluno a ser atualizado
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);
			if (aluno == null)
			{
				throw new ArgumentException($"Aluno com CPF {cpf} não encontrado", nameof(cpf));
			}

			var turmas =
				await _context.Turmas
				.Where(t => alunoRequest.NumerosTurmas.Contains(t.Numero))
				.Include(t => t.Alunos)
				.ToListAsync();

			// Verifica se cada turma tem menos de 5 alunos (Restrição 4)
			foreach (var turma in turmas)
			{
				if (turma.Alunos.Count >= 5)
				{
					throw new InvalidOperationException($"Não é possível matricular o aluno {aluno.Nome} na turma {turma.Nome} porque já atingiu o número máximo de alunos.");
				}
			}

			aluno.Nome = alunoRequest.Nome;
			aluno.Email = alunoRequest.Email;
			aluno.Turmas = turmas;

			// Salva as alteraçoes feitas retornando o aluno atualizado
			await _context.SaveChangesAsync();
			return await Get(aluno.CPF);
		}

		// Método para excluir o aluno
		public async Task<Aluno> Delete(string cpf)
		{
			var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.CPF == cpf);

			// Verifica se o aluno existe
			if (aluno == null)
			{
				throw new ArgumentException($"Aluno com CPF {cpf} não encontrado", nameof(cpf));
			}

			// Restrição do aluno estar matriculado em alguma turma
			if (aluno.Turmas.Count > 0)
			{
				throw new InvalidOperationException($"O aluno {aluno.Nome} está matriculado em {aluno.Turmas.Count} turmas e não pode ser excluído.");
			}

			// Remove o aluno
			_context.Alunos.Remove(aluno);
			await _context.SaveChangesAsync();

			return aluno;
		}
	}
}
