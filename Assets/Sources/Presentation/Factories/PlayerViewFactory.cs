using Match3.Presentation.Assets.Sources.Presentation.Views.Players;
using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Factories
{
    public class PlayerViewFactory
    {
        public PlayerView Create()
        {
            return Object.FindObjectOfType<PlayerView>();  //TODO: change
        }
    }
}
