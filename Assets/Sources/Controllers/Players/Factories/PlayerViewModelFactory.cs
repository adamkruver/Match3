using Match3.Domain.Assets.Sources.Domain.Players;

namespace Match3.Controllers.Assets.Sources.Controllers.Players.Factories
{
    public class PlayerViewModelFactory
    {
        public PlayerViewModel Create(Player player)
        {
            return new PlayerViewModel(player);
        }
    }
}