using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Api.DTOs;
using WebDosimetro.Shared;

namespace WebDosimetro.Api.Mappings
{
    public class Map: Profile
    {
        public Map()
        {
            CreateMap<Drug, DrugDTO>().ReverseMap();
        }
    }
}
