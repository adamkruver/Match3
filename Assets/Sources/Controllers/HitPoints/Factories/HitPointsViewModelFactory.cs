using Match3.Domain.Assets.Sources.Domain.Players;

namespace Match3.Controllers.Assets.Sources.Controllers.HitPoints.Factories
{
    public class HitPointsViewModelFactory
    {
        public HitPointsViewModel Create(Player player)
        {
            return new HitPointsViewModel(player);
        }
    }
}
