using AutoMapper;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(
                    destino => destino.Nome, 
                    option => option.MapFrom(origem => $"{origem.Nome} {origem.Sobrenome}")
                )
                .ForMember(
                    destino => destino.Idade,
                    option => option.MapFrom(origem => origem.DataNascimento.ObterIdadeAtual())
                );

            CreateMap<AlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoRegisterDTO>().ReverseMap();
        }
    }
}
