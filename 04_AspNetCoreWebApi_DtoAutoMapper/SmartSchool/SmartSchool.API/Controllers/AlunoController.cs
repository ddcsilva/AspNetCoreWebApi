using AutoMapper;
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
        private readonly IMapper mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = this.repository.ListarAlunos(true);
            var alunosDTO = this.mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            return Ok(alunosDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = this.repository.ConsultarAlunoPorId(id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            var alunoDTO = this.mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegisterDTO alunoDTO)
        {
            var aluno = this.mapper.Map<Aluno>(alunoDTO);

            this.repository.Incluir(aluno);

            if (this.repository.Efetivar())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", mapper.Map<AlunoDTO>(aluno));
            }

            return BadRequest("Aluno não cadastrado!");
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegisterDTO alunoDTO)
        {
            var aluno = this.repository.ConsultarAlunoPorId(id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.mapper.Map(alunoDTO, aluno);

            this.repository.Atualizar(aluno);

            if (this.repository.Efetivar())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", mapper.Map<AlunoDTO>(aluno));   
            }

            return BadRequest("Aluno não atualizado!");
        }

        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegisterDTO alunoDTO)
        {
            var aluno = this.repository.ConsultarAlunoPorId(id);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            this.mapper.Map(alunoDTO, aluno);

            this.repository.Atualizar(aluno);

            if (this.repository.Efetivar())
            {
                return Created($"/api/aluno/{alunoDTO.Id}", mapper.Map<AlunoDTO>(aluno));
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
