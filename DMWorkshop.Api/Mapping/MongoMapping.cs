using DMWorkshop.Model.Core;
using DMWorkshop.Model.Creatures;
using DMWorkshop.Model.Items;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMWorkshop.Api.Mapping
{
    public static class MongoMapping
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<Skill>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Size>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<ItemSlot>(BsonType.String));

            BsonClassMap.RegisterClassMap<Creature>(m =>
            {
                m.MapIdMember(c => c.Name).SetIdGenerator(StringObjectIdGenerator.Instance);
                m.MapMember(c => c.Size).SetSerializer(new EnumSerializer<Size>(BsonType.String));
                m.MapMember(c => c.Level);
                m.MapMember(c => c.Characteristics);
                m.MapMember(c => c.Gear);
                m.MapMember(c => c.Skills);
                m.MapCreator(c => new Creature(c.Name, c.Characteristics, c.Size, c.Level, c.Gear, c.Skills));
            });

            BsonClassMap.RegisterClassMap<Gear>(m =>
            {
                m.MapIdMember(x => x.Name).SetIdGenerator(StringObjectIdGenerator.Instance);
                m.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<Armor>(m =>
            {
                m.MapMember(x => x.ArmorSlot).SetSerializer(new EnumSerializer<ItemSlot>(BsonType.String));
                m.MapMember(x => x.AC);
                m.MapMember(x => x.DexModLimit);
                m.MapCreator(x => new Armor(x.Name, x.ArmorSlot, x.AC, x.DexModLimit));
            });
        }
    }
}
