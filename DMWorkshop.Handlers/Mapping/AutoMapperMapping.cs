using AutoMapper;
using DMWorkshop.DTO.Characters;
using DMWorkshop.Model.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Handlers.Mapping
{
    public class AutoMapperMapping : Profile
    {
        public AutoMapperMapping()
        {
            CreateMap<Creature, CreatureReadModel>()
                .ForMember(x => x.Attacks, o => o.MapFrom(x => x.ModifiedAttacks));
        }
    }
}
