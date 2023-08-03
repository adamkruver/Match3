using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Domain.Components;

namespace Match3.Domain.Units
{
    public class Unit : Composite, IUnit
    {
        public Unit(IUnitType unitType)
        {
            UnitType = unitType;
        }

        public IUnitType UnitType { get; }
    }
}
