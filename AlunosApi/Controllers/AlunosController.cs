using AlunosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : Controller
    {
        private readonly IAlunoService _service;

        public AlunosController(IAlunoService service)
        {
            _service = service;
        }
    }
}
