using Kruver.Mvvm.Factories;
using Match3.Presentation.Assets.Sources.Presentation.Views.HitPoints;

namespace Match3.Presentation.Assets.Sources.Presentation.Factories
{
    public class HitPointsBarViewFactory
    {
        private readonly ViewFactory _viewFactory;
        private static readonly string _path = @"Views/Hud/Prefabs/";

        public HitPointsBarViewFactory(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public HitPointsBarView Create()
        {
            return _viewFactory.Create<HitPointsBarView>(_path);
        }
    }
}
