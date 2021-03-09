using AutoMapper;
using StocksMasterAPI.DTOs;
using StocksMasterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StocksDatum, StocksDatumDto>();
        }
    }
}
