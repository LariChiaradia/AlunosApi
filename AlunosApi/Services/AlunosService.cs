using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            var aluno =await _context.Alunos.ToListAsync();
            return aluno;
        }
        public async Task<Aluno> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno is null)
                throw new Exception($"O Aluno de id={id} não foi localizado");

            return aluno;
        }
        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;
            if(!string.IsNullOrEmpty(nome))
            {
                alunos = _context.Alunos.Where(a =>  a.Nome.Contains(nome)).ToList();
            }
            else
            {
                throw new Exception($"O Aluno de Nome={nome} não foi localizado");
            }
            return alunos;
        }
        public async Task AddAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
