namespace EFIdiomasAPI.DTOs
{
    public record struct CreateTurmaDto(
        string Nome,
        string Numero,
        string AnoLetivo,
        List<string> CPFAlunos
    );


}
