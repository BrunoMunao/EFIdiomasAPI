namespace EFIdiomasAPI.Models
{
	public class Turma
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Numero { get; set; }
		public string AnoLetivo { get; set; }
		public List<Aluno> Alunos { get; set; }
	}
}
