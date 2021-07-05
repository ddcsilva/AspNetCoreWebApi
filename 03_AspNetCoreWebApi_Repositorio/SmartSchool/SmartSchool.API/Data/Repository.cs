using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public void Incluir<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }
        public void Atualizar<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }

        public void Excluir<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public bool Efetivar()
        {
            return this.context.SaveChanges() > 0;
        }

        public Aluno[] ListarAlunos(bool incluiProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (incluiProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] ListarAlunosPorDisciplina(int disciplinaId, bool incluiProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (incluiProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno ConsultarAlunoPorId(int alunoId, bool incluiProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;

            if (incluiProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] ListarProfessores(bool incluiAlunos = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (incluiAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Professor[] ListarProfessoresPorDisciplina(int disciplinaId, bool incluiAlunos = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (incluiAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(aluno => aluno.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor ConsultarProfessorPorId(int professorId, bool incluiProfessores = false)
        {
            IQueryable<Professor> query = this.context.Professores;

            if (incluiProfessores)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}
