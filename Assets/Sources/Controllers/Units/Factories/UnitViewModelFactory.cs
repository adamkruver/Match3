using Match3.Domain.Units;

namespace Match3.Controllers.Assets.Sources.Controllers.Units.Factories
{
    public class UnitViewModelFactory
    {
        public UnitViewModel Create(IUnit unit)
        {
            return new UnitViewModel(unit);
        }
    }
}
