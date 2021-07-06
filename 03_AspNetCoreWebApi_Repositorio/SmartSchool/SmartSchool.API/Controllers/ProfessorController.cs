using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repository;

        public ProfessorController(IRepository repository) 
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = this.repository.ListarProfessores(true);

            return Ok(professores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = this.repository.ConsultarProfessorPorId(id);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            this.repository.Incluir(professor);

            if (this.repository.Efetivar())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorEncontrado = this.repository.ConsultarProfessorPorId(id);

            if (professorEncontrado == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.repository.Atualizar(professor);

            if (this.repository.Efetivar())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var professorEncontrado = this.repository.ConsultarProfessorPorId(id);

            if (professorEncontrado == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.repository.Atualizar(professor);

            if (this.repository.Efetivar())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = this.repository.ConsultarProfessorPorId(id);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.repository.Excluir(professor);

            if (this.repository.Efetivar())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
        }
    }
}
