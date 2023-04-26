using System.ComponentModel.DataAnnotations;

namespace EFIdiomasAPI.Entities
{
    public class Aluno
    {
        [Required]
        public string Nome { get; set; }
        [Key]
        [Required]
        public string CPF { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public List<Turma> Turmas { get; set; }
    }
}
