using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : Controller
    {
        private readonly IAlunoService _service;

        public AlunosController(IAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _service.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunosByName(string nome)
        {
            try
            {
                var alunos = await _service.GetAlunosByNome(nome);
                if (!alunos.Any())
                    return NotFound($"Não existe alunos com o critério {nome}");
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpGet("{id:int}", Name = "GetAlunoById")]
        public async Task<ActionResult<Aluno>> GetAlunoById(int id)
        {
            try
            {
                var aluno = await _service.GetAlunoById(id);
                if (aluno is null)
                    return NotFound($"Não existe aluno com o id={id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAluno(Aluno aluno)
        {
            try
            {
                await _service.AddAluno(aluno);
                return CreatedAtRoute(nameof(GetAlunoById), new {id = aluno.AlunoId}, aluno);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAluno(int id, Aluno aluno)
        {
            try
            {
                if(aluno.AlunoId == id)
                {
                   await _service.UpdateAluno(aluno);
                    return Ok($"Aluno com id={id} foi atulizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _service.GetAlunoById(id);
                if (aluno != null)
                {
                    await _service.DeleteAluno(aluno);
                    return Ok($"Aluno de id={id} foi excluido com sucesso.");
                }
                else
                {
                    return NotFound($"Aluno com id={id} não encontrado!");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
    }
}
