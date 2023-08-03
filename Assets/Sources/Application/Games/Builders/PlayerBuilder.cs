using Assets.Sources.Infrastructure.Factories;
using Kruver.Mvvm.Factories;
using Match3.Controllers.Assets.Sources.Controllers.HitPoints;
using Match3.Controllers.Assets.Sources.Controllers.HitPoints.Factories;
using Match3.Controllers.Assets.Sources.Controllers.Players;
using Match3.Controllers.Assets.Sources.Controllers.Players.Factories;
using Match3.Controllers.Assets.Sources.Controllers.Units;
using Match3.Controllers.Assets.Sources.Controllers.Units.Factories;
using Match3.Domain.Assets.Sources.Domain.Players;
using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Domain.Units;
using Match3.Domain.Units.Builders;
using Match3.Domain.Units.Factories;
using Match3.Presentation.Assets.Sources.Presentation.Factories;
using Match3.Presentation.Assets.Sources.Presentation.Views.GamePlayHud;
using Match3.Presentation.Assets.Sources.Presentation.Views.HitPoints;
using Match3.Presentation.Assets.Sources.Presentation.Views.Players;
using Match3.Presentation.Assets.Sources.Presentation.Views.Units;
using System.Collections.Generic;

namespace Match3.Application.Assets.Sources.Application.Games.Builders
{
    public class PlayerBuilder
    {
        private readonly PlayerFactory _playerFactory = new PlayerFactory();
        private readonly PlayerViewModelFactory _playerViewModelFactory = new PlayerViewModelFactory();
        private readonly UnitViewModelFactory _unitViewModelFactory = new UnitViewModelFactory();
        private readonly UnitViewFactory _unitViewFactory;
        private readonly HitPointsBarViewFactory _hitPointsBarViewFactory;
        private readonly HitPointsViewModelFactory _hitPointsViewModelFactory = new HitPointsViewModelFactory();
        private readonly UnitDirector _unitDirector;
        private readonly GameplayHudView _gameplayHudView;

        public PlayerBuilder(ViewFactory viewFactory, UnitDirector unitDirector, GameplayHudView gameplayHudView)
        {
            _unitViewFactory = new UnitViewFactory(viewFactory);
            _hitPointsBarViewFactory = new HitPointsBarViewFactory(viewFactory);
            _unitDirector = unitDirector;
            _gameplayHudView = gameplayHudView;
        }

        public void BuildLeft(IEnumerable<IUnitType> unitTypes)
        {
            Build(unitTypes, _gameplayHudView.LeftPlayer);
        }

        public void BuildRight(IEnumerable<IUnitType> unitTypes)
        {
            Build(unitTypes, _gameplayHudView.RightPlayer);
        }

        private void Build(IEnumerable<IUnitType> unitTypes, PlayerView playerView)
        {
            List<IUnit> units = new List<IUnit>();

            foreach (var unitType in unitTypes)
            {
                IUnit unit = _unitDirector.Build(unitType);
                units.Add(unit);

                UnitViewModel unitViewModel = _unitViewModelFactory.Create(unit);
                UnitView unitView = _unitViewFactory.Create(unitType);
                HitPointsViewModel hitPointsViewModel = _hitPointsViewModelFactory.Create(unit);
                HitPointsBarView hitPointsBarView = _hitPointsBarViewFactory.Create();

                unitView.Bind(unitViewModel);
                hitPointsBarView.Bind(hitPointsViewModel);
                playerView.AddChild(unitView, hitPointsBarView);
            }

            Player player = _playerFactory.Create(units);
            PlayerViewModel playerViewModel = _playerViewModelFactory.Create(player);
        }
    }
}
