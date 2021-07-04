using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly DataContext context;

        public ProfessorController(DataContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Professores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = this.context.Professores.FirstOrDefault(p => p.Id == id);

            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            this.context.Add(professor);
            this.context.SaveChanges();

            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorEncontrado = this.context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (professorEncontrado == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.context.Update(professor);
            this.context.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var professorEncontrado = this.context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (professorEncontrado == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.context.Update(professor);
            this.context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professorEncontrado = this.context.Professores.FirstOrDefault(a => a.Id == id);

            if (professorEncontrado == null)
            {
                return BadRequest("Professor não encontrado");
            }

            this.context.Remove(professorEncontrado);
            this.context.SaveChanges();

            return Ok();
        }
    }
}
