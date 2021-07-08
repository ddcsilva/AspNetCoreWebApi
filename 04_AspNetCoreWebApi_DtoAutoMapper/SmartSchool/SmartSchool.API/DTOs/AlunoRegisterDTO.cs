using System;

namespace SmartSchool.API.DTOs
{
    public class AlunoRegisterDTO
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicioMatricula { get; set; } = DateTime.Now;
        public DateTime? DataFimMatricula { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}
