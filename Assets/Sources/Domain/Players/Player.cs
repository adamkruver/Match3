using Match3.Domain.Units;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Domain.Assets.Sources.Domain.Players
{
    public class Player
    {
        public Player(IEnumerable<IUnit> units) 
        {
            Units = units.ToArray();
        }

        public IUnit[] Units { get; }
    }
}