
using Match3.Domain.Assets.Sources.Domain.Units.Types;

namespace Match3.Domain.Units
{
    public interface IUnit
    {
        IUnitType UnitType { get; }
    }
}
