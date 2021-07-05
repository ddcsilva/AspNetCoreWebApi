using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Incluir<T>(T entity) where T : class;
        void Atualizar<T>(T entity) where T : class;
        void Excluir<T>(T entity) where T : class;
        bool Efetivar();

        public Aluno[] ListarAlunos(bool incluiProfessor = false);
        public Aluno[] ListarAlunosPorDisciplina(int disciplinaId, bool incluiProfessor = false);
        public Aluno ConsultarAlunoPorId(int alunoId, bool incluiProfessor = false);

        public Professor[] ListarProfessores(bool incluiAlunos = false);
        public Professor[] ListarProfessoresPorDisciplina(int disciplinaId, bool incluiAlunos = false);
        public Professor ConsultarProfessorPorId(int professorId, bool incluiProfessores = false);
    }
}
