using AutoMapper;
using ByteBabbelAPI.Data.Dtos;
using ByteBabbelAPI.Models;

namespace ByteBabbelAPI.Profiles
{
    public class BabbelProfile : Profile
    {
        public BabbelProfile()
        {
            //Mapeamento Aluno
            CreateMap<CreateAlunoDto, Aluno>();
            CreateMap< Aluno, ReadAlunoDto>();
            CreateMap<UpdateAlunoDto, Aluno>();

            //Mapeamento Turma
            CreateMap<CreateTurmaDto, Turma>();
            CreateMap<Turma, ReadTurmaDto>();
            CreateMap<UpdateTurmaDto, Turma>();

            //Mapeamento Matricula
            CreateMap<Matricula, ReadMatriculaDto>();
        }
    }
}
