using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Application.DTOs;
using Antlia.Domain.Entities;
using AutoMapper;

namespace Antlia.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<MovimentoManual, MovimentoManualDTO>().ReverseMap();
            CreateMap<ProdutoCosif, ProdutoCosifDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
