using DMWorkshop.Model.Characters;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Mapping
{
    public static class MongoMapping
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<Ability>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Skill>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Size>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<ItemSlot>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<Speed>(BsonType.String));
            
            BsonClassMap.RegisterClassMap<Character>(m =>
            {
                m.MapIdMember(x => x.Name).SetIdGenerator(StringObjectIdGenerator.Instance);
                m.SetIsRootClass(true);
                m.MapMember(c => c.Size);
                m.MapMember(c => c.Speed);
                m.MapMember(c => c.Level);
                m.MapMember(c => c.CR);
                m.MapMember(c => c.Scores);
                m.MapMember(c => c.Gear);
                m.MapMember(c => c.Skills);
                m.MapMember(c => c.Expertise);
            });

            BsonClassMap.RegisterClassMap<Creature>(m =>
            {
                m.MapMember(c => c.Saves);
                m.MapCreator(c => new Creature(c.Name, c.Scores, c.Size, c.Speed, c.Level, c.CR, c.Gear, c.Saves, c.Skills, c.Expertise));
            });

            BsonClassMap.RegisterClassMap<Player>(m =>
            {
                m.MapMember(c => c.Class);
                m.MapMember(c => c.Race);
                m.MapMember(c => c.MaxHP);
                m.MapCreator(c => new Player(c.Name, c.Scores, c.Class, c.Race, c.MaxHP, c.Level, c.Gear, c.Skills, c.Expertise));
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
