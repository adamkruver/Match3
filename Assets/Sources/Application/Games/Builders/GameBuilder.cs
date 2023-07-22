using Kruver.Mvvm.Factories;
using Match3.Presentation.Sources.Presentation.Factories;
using Sources.Infrastructure.Services;

namespace Match3.Application.Builders
{
    public class GameBuilder
    {
        public Game Build()
        {
            SelectableService selectableService = new SelectableService();
            ViewTypeFactory viewTypeFactory = new ViewTypeFactory();

            ViewFactory viewFactory = new ViewFactory();

            return new Game(viewFactory, selectableService, viewTypeFactory);
        }
    }
}