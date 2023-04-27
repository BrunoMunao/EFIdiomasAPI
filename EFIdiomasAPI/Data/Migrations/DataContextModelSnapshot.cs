﻿// <auto-generated />
using EFIdiomasAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFIdiomasAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlunoTurma", b =>
                {
                    b.Property<string>("AlunosCPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TurmasNumero")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlunosCPF", "TurmasNumero");

                    b.HasIndex("TurmasNumero");

                    b.ToTable("AlunoTurma", (string)null);
                });

            modelBuilder.Entity("EFIdiomasAPI.Entities.Aluno", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPF");

                    b.ToTable("Alunos", (string)null);
                });

            modelBuilder.Entity("EFIdiomasAPI.Entities.Turma", b =>
                {
                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AnoLetivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Numero");

                    b.ToTable("Turmas", (string)null);
                });

            modelBuilder.Entity("AlunoTurma", b =>
                {
                    b.HasOne("EFIdiomasAPI.Entities.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunosCPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFIdiomasAPI.Entities.Turma", null)
                        .WithMany()
                        .HasForeignKey("TurmasNumero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
