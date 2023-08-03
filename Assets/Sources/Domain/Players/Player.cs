using Match3.Domain.Assets.Sources.Domain.Units.Components;
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

        public IEnumerable<IUnit> Units { get; }

        public void Select(IUnit selectedUnit)
        {
            if (Units.Contains(selectedUnit) == false)
                return;

            foreach (IUnit unit in Units)
            {
                SelectableComponent selectable = unit.Get<SelectableComponent>();

                if (selectedUnit == unit)
                    selectable.Select();
                else
                    selectable.Unselect();
            }
        }
    }
}