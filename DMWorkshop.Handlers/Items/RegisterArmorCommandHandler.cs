using DMWorkshop.DTO.Items;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Items
{
    public class RegisterArmorCommandHandler : AsyncRequestHandler<RegisterArmorCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterArmorCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(RegisterArmorCommand command, CancellationToken cancellationToken)
        {
            var armor = new Armor(
                command.Name,
                command.Slot,
                command.AC,
                command.DexModLimit,
                command.AdditionalModifiers
                );

            return _database.Save("gear", x => x.Name == armor.Name, armor, cancellationToken);
        }
    }
}
