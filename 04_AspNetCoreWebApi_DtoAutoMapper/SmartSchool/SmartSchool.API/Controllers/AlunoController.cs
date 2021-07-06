using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;
using System.Collections.Generic;

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
            var alunos = this.repository.ListarAlunos(true);
            var alunosRetorno = new List<AlunoDTO>();

            foreach (var aluno in alunos)
            {
                alunosRetorno.Add(new AlunoDTO()
                {
                    Id = aluno.Id,
                    Matricula = aluno.Matricula,
                    Nome = $"{aluno.Nome} {aluno.Sobrenome}",
                    Telefone = aluno.Telefone,
                    //DataNascimento = aluno.DataNascimento,
                    DataInicioMatricula = aluno.DataInicioMatricula,
                    Ativo = aluno.Ativo
                });
            }

            return Ok(alunosRetorno);
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
