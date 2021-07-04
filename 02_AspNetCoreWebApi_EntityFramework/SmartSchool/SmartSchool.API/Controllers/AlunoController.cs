using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext context;

        public AlunoController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Alunos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => 
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );

            if (aluno == null)
            {
                return BadRequest("O aluno não foi encontrado");
            }

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.context.Add(aluno);
            this.context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoEncontrado = this.context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (alunoEncontrado == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.context.Update(aluno);
            this.context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoEncontrado = this.context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (alunoEncontrado == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.context.Update(aluno);
            this.context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.context.Remove(aluno);
            this.context.SaveChanges();
            return Ok();
        }
    }
}
