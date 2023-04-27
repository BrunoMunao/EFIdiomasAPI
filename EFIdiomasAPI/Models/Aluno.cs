using System.ComponentModel.DataAnnotations;

namespace EFIdiomasAPI.Entities
{
	public class Aluno
	{
		// O nome completo do aluno
		[Required(ErrorMessage = "O nome completo é obrigatório.")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome completo deve ter entre 3 e 100 caracteres.")]
		public string Nome { get; set; }

		// O CPF do aluno (PK e FK).
		[Key]
		[Required(ErrorMessage = "O CPF é obrigatório.")]
		[RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 'xxx.xxx.xxx-xx'.")]
		public string CPF { get; set; }

		// O e-mail do aluno.
		[Required(ErrorMessage = "O e-mail é obrigatório.")]
		[EmailAddress(ErrorMessage = "O e-mail deve estar no formato correto.")]
		public string Email { get; set; }

		// As turmas em que o aluno está matriculado.
		[Required(ErrorMessage = "O aluno deve estar matriculado em pelo menos uma turma.")]
		public List<Turma> Turmas { get; set; } = new List<Turma>();
	}

}
