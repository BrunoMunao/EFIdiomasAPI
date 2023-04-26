namespace EFIdiomasAPI.Application.DTOs
{
    public record struct UpdateTurmaDto
    (
        string Nome,
        string AnoLetivo,
        List<string> CPFAlunos
    );
}
