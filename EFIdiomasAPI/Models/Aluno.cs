using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFIdiomasAPI.Models
{
	public class Aluno
	{
		public string Nome { get; set; }
		[Key]
		[Required]
		public string CPF { get; set;}
		public string Email { get; set; }
		[JsonIgnore]
		public List<Turma> Turmas { get; set; }	
	}
}
