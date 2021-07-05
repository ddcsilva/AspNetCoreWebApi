using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository repository;

        public AlunoController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resultado = this.repository.ListarAlunos(true);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = this.repository.ConsultarAlunoPorId(id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.repository.Incluir(aluno);

            if (this.repository.Efetivar())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoEncontrado = this.repository.ConsultarAlunoPorId(id);

            if (alunoEncontrado == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.repository.Atualizar(aluno);

            if (this.repository.Efetivar())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoEncontrado = this.repository.ConsultarAlunoPorId(id);

            if (alunoEncontrado == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.repository.Atualizar(aluno);

            if (this.repository.Efetivar())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = this.repository.ConsultarAlunoPorId(id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.repository.Excluir(aluno);

            if (this.repository.Efetivar())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não excluído!");
        }
    }
}
