using Assets.Sources.InfrastructureInterfaces.Factories;
using Match3.Domain.Assets.Sources.Domain.Players;
using Match3.Domain.Units;
using System.Collections.Generic;

namespace Assets.Sources.Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public Player Create(IEnumerable<IUnit> units)
        {
            return new Player(units);
        }
    }
}
