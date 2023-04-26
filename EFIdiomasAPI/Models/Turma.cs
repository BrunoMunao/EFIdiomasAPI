using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFIdiomasAPI.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class Turma
    {
        [Required]
        public string Nome { get; set; }
        [Key]
        [Required]
        public string Numero { get; set; }
        [Required]
        public string AnoLetivo { get; set; }
        [Required]
        public List<Aluno> Alunos { get; set; }
    }
}
