using Match3.Domain.Assets.Sources.Domain.Units.Components;
using Match3.Domain.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Domain.Assets.Sources.Domain.Players
{
    public class Player
    {
        public Player(IEnumerable<IUnit> units)
        {
            Units = units;
        }

        public event Action SelectChanged;

        public IEnumerable<IUnit> Units { get; }
        public IUnit Selected { get; private set; }
        public IUnit[] Unselecteds => Units.Where(unit => unit != Selected).ToArray();

        public void Select(IUnit selectedUnit)
        {
            if (Units.Contains(selectedUnit) == false)
                return;

            foreach (IUnit unit in Units)
            {
                SelectableComponent selectable = unit.Get<SelectableComponent>();

                if (selectedUnit == unit)
                {
                    selectable.Select();
                    Selected = unit;
                }
                else
                { 
                    selectable.Unselect();
                }
            }

            SelectChanged?.Invoke();
        }
    }
}