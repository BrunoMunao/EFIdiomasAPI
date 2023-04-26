namespace EFIdiomasAPI.DTOs
{
    public record struct UpdateAlunoDto
    (
        string Nome,
        string Email,
        List<string> NumerosTurmas
    );
}
