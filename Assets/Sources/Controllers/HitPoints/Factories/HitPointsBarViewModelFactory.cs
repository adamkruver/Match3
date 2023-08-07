using Kruver.Mvvm.ViewModels;
using Match3.Domain.Units;

namespace Match3.Controllers.Assets.Sources.Controllers.HitPoints.Factories
{
    public class HitPointsBarViewModelFactory
    {
        public HitPointsBarViewModel Create(IUnit unit)
        {
            return new HitPointsBarViewModel(unit);
        }
    }
}
