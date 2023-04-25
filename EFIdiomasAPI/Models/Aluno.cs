namespace EFIdiomasAPI.Models
{
	public class Aluno
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string CPF { get; set;}
		public string Email { get; set; }
		public List<Turma> Turmas { get; set; }	
	}
}
