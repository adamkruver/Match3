using Match3.Domain.Assets.Sources.Domain.Players;
using Match3.Domain.Units;

namespace Match3.Controllers.Assets.Sources.Controllers.Units.Factories
{
    public class UnitViewModelFactory
    {
        public UnitViewModel Create(IUnit unit, Player player)
        {
            return new UnitViewModel(unit, player);
        }
    }
}
