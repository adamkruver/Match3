using Match3.Presentation.Assets.Sources.Presentation.Views.GamePlayHud;
using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Factories
{
    public class GameplayHudViewFactory
    {
        private static readonly string _path = @"Views/Hud/Prefabs/";

        public GameplayHudView Create()
        {
            return Object.Instantiate(Resources.Load<GameplayHudView>(_path + typeof(GameplayHudView).Name));
        }
    }
}
