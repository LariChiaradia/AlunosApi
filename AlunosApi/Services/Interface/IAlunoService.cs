﻿using AlunosApi.Models;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoById(int id);
        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome);
        Task AddAluno(Aluno aluno);
        Task<Aluno> UpdateAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
    }
}
