using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BioEasy.Data.Entities;
using BioEasy.ViewModels;

namespace BioEasy.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Paciente, Prevencao>().ReverseMap();
            CreateMap<Paciente, Progresso>().ReverseMap();
            CreateMap<Paciente, AnaliseAtual>().ReverseMap();
        }
    }
}
