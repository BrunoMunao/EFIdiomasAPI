using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFIdiomasAPI.Models
{
	[Route("api/[controller]")]
	[ApiController]
	public class Turma
	{
		public string Nome { get; set; }
		[Key]
		[Required]
		public string Numero { get; set; }
		public string AnoLetivo { get; set; }
		[JsonIgnore]
		public List<Aluno> Alunos { get; set; }
	}
}
