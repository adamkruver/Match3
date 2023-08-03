using Assets.Sources.Infrastructure.Factories;
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
using Match3.Presentation.Assets.Sources.Presentation.Views.Players;
using Match3.Presentation.Assets.Sources.Presentation.Views.Units;
using System.Collections.Generic;

namespace Match3.Application.Assets.Sources.Application.Games.Builders
{
    public class PlayerBuilder
    {
        private readonly PlayerFactory _playerFactory = new PlayerFactory();
        private readonly PlayerViewModelFactory _playerViewModelFactory = new PlayerViewModelFactory();
        private readonly PlayerViewFactory _playerViewFactory = new PlayerViewFactory();
        private readonly UnitViewModelFactory _unitViewModelFactory = new UnitViewModelFactory();
        private readonly UnitViewFactory _unitViewFactory;
        private readonly UnitDirector _unitDirector;

        public PlayerBuilder(UnitViewFactory unitViewFactory, UnitDirector unitDirector) 
        {
            _unitViewFactory = unitViewFactory;
            _unitDirector = unitDirector;
        }

        public void BuildLeft(IEnumerable<IUnitType> unitTypes)
        {
            List<IUnit> units = new List<IUnit>();
            PlayerView playerView = _playerViewFactory.Create();
            
            foreach (var unitType in unitTypes)
            {
                IUnit unit = _unitDirector.Build(unitType);
                units.Add(unit);

                UnitViewModel unitViewModel = _unitViewModelFactory.Create(unit);
                UnitView unitView = _unitViewFactory.Create(unitType);

                unitView.Bind(unitViewModel);
                playerView.AddChild(unitView);
            }

            Player player = _playerFactory.Create(units);
            PlayerViewModel playerViewModel = _playerViewModelFactory.Create(player);
        }
    }
}
