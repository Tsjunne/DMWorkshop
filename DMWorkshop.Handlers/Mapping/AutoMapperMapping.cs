using AutoMapper;
using DMWorkshop.DTO.Characters;
using DMWorkshop.DTO.Items;
using DMWorkshop.Model.Characters;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
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
            CreateMap<Armor, ArmorInfo>()
                .ForMember(x => x.Slot, o => o.MapFrom(x => x.ArmorSlot));
            CreateMap<Die, int>().ConvertUsing(d => d.Sides);
        }
    }
}
