using Kruver.Mvvm.Factories;
using Match3.Presentation.Sources.Presentation.Factories;
using Sources.Infrastructure.Factories;

namespace Match3.Application.Builders
{
    public class GameBuilder
    {
        public Game Build()
        {
            ViewTypeFactory viewTypeFactory = new ViewTypeFactory();
            ViewFactory viewFactory = new ViewFactory();
            
            // Infrastructure Factories
            CellFactory cellFactory = new CellFactory();

            // Infrastructure Factories
            TableFactory tableFactory = new TableFactory(cellFactory, viewTypeFactory, viewFactory);



            return new Game(tableFactory);
        }
    }
}