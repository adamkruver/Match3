using Kruver.Mvvm.Factories;
using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Presentation.Assets.Sources.Presentation.Views.Units;

namespace Match3.Presentation.Assets.Sources.Presentation.Factories
{
    public class UnitViewFactory
    {
        private readonly ViewFactory _viewFactory;
        private readonly string _unitViewPath = @"Views/Units/Prefabs/";

        public UnitViewFactory(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public UnitView Create(IUnitType unitType)
        {
            return _viewFactory.Create<UnitView>(_unitViewPath, unitType.GetType().Name);
        }
    }
}
