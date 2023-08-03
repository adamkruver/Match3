using Kruver.Mvvm.ViewModels;
using Match3.Domain.Assets.Sources.Domain.Players;

namespace Match3.Controllers.Assets.Sources.Controllers.Players
{
    public class PlayerViewModel : ViewModel<Player>
    {
        public PlayerViewModel(Player model) : base(model)
        {
        }

        protected override void OnDisable()
        {
            
        }

        protected override void OnEnable()
        {
            
        }
    }
}
