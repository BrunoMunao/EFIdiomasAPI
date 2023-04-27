using System.ComponentModel.DataAnnotations;

namespace EFIdiomasAPI.Entities
{
    public class Aluno
    {

		// O nome completo do aluno
		[Required]
        public string Nome { get; set; }

		// O CPF do aluno (PK e FK).
		[Key]
        [Required]
        public string CPF { get; set; }

		// O e-mail do aluno.
		[Required]
        [EmailAddress]
        public string Email { get; set; }

		// As turmas em que o aluno está matriculado.
		[Required]
        public List<Turma> Turmas { get; set; } = new List<Turma>();
	}
}
