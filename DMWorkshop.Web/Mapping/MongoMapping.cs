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

namespace DMWorkshop.Web.Mapping
{
    public static class MongoMapping
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<Ability>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Skill>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Size>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<ItemSlot>(BsonType.String));

            BsonClassMap.RegisterClassMap<Creature>(m =>
            {
                m.MapIdMember(c => c.Name).SetIdGenerator(StringObjectIdGenerator.Instance);
                m.MapMember(c => c.Size);
                m.MapMember(c => c.Level);
                m.MapMember(c => c.CR);
                m.MapMember(c => c.Scores);
                m.MapMember(c => c.Gear);
                m.MapMember(c => c.Saves);
                m.MapMember(c => c.Skills);
                m.MapCreator(c => new Creature(c.Name, c.Scores, c.Size, c.Level, c.CR, c.Gear, c.Saves, c.Skills));
            });

            BsonClassMap.RegisterClassMap<Gear>(m =>
            {
                m.MapIdMember(x => x.Name).SetIdGenerator(StringObjectIdGenerator.Instance);
                m.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<Armor>(m =>
            {
                m.MapMember(x => x.ArmorSlot);
                m.MapMember(x => x.AC);
                m.MapMember(x => x.DexModLimit);
                m.MapCreator(x => new Armor(x.Name, x.ArmorSlot, x.AC, x.DexModLimit));
            });
        }
    }
}
