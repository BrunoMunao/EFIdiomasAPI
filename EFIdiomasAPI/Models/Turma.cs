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
		// Nome da turma (Básico, Intermediário e Avançado)
		[Required(ErrorMessage = "O nome da turma é obrigatório.")]
		[DefaultValue("Básico")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da turma deve ter entre 3 e 100 caracteres.")]
		public string Nome { get; set; }

		// O número da turma. Está como String, pois futuramente pode ser que exista uma turma 001 e 001B
		[Key]
		[Required(ErrorMessage = "O número da turma é obrigatório.")]
		[DefaultValue("0")]
		public string Numero { get; set; }

		// O ano letivo da turma.
		[Required(ErrorMessage = "O ano letivo da turma é obrigatório.")]
		[RegularExpression(@"^\d{4}$", ErrorMessage = "O ano letivo deve conter 4 dígitos.")]
		[DefaultValue("2023")]
		public string AnoLetivo { get; set; }

		// Os alunos matriculados na turma.
		public List<Aluno> Alunos { get; set; } = new List<Aluno>();
	}

}
