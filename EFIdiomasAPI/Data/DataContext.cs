﻿using EFIdiomasAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFIdiomasAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
    }
}
