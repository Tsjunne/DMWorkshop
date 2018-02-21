using DMWorkshop.DTO.Items;
using DMWorkshop.Model.Items;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Items
{
    public class RegisterArmorCommandHandler : IRequestHandler<RegisterArmorCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterArmorCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public Task Handle(RegisterArmorCommand command, CancellationToken cancellationToken)
        {
            var armor = new Armor(
                command.Name,
                Enum.Parse<ItemSlot>(command.Slot),
                command.AC,
                command.DexModLimit
                );

            return _database.Save("gear", x => x.Name == armor.Name, armor);
        }
    }
}
