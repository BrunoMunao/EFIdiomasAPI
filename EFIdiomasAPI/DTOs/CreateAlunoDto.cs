﻿namespace EFIdiomasAPI.DTOs
{
    public record struct CreateAlunoDto(
        string Nome,
        string CPF,
        string Email,
        List<string> NumerosTurmas
    );
}
