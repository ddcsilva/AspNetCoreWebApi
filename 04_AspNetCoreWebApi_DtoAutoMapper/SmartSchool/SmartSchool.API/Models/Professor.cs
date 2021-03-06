using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Professor
    {
        public Professor() { }

        public Professor(int id, int registro, string nome, string sobrenome, string telefone = null)
        {
            Id = id;
            Registro = registro;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
        }

        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicioRegistro { get; set; } = DateTime.Now;
        public DateTime? DataFimRegistro { get; set; } = null;
        public bool Ativo { get; set; } = true;

        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
