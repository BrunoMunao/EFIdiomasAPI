using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace EFIdiomasAPI.Entities
{
	public class Turma
	{
		// Nome da turma (Báscio, Intermediário e Avançado)
		[Required]
		[DefaultValue("Básico")]
		public string Nome { get; set; }

		// O número da turma. Está como String, pois futuramente pode ser que exista uma turma 001 e 001B
		[Key]
		[Required]
		[DefaultValue("0")]
		public string Numero { get; set; }

		// O ano letivo da turma.
		[Required]
		[DefaultValue("2023")]
		public string AnoLetivo { get; set; }

		// Os alunos matriculados na turma.
		[Required]
		public List<Aluno> Alunos { get; set; } = new List<Aluno>();
	}
}
