using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    AlunoId = 1,
                    Nome = "Larissa Santos",
                    Email = "larissa.santos@gmail.com",
                    Idade = 23
                },
                new Aluno
                {
                    AlunoId = 2,
                    Nome = "Luiza Souza",
                    Email = "luiza.souza@gmail.com",
                    Idade = 22
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
