using Kruver.Mvvm.Factories;

namespace Match3.Application.Builders
{
    public class GameBuilder
    {
        public Game Build()
        {
            ViewFactory viewFactory = new ViewFactory();

            return new Game(viewFactory);
        }
    }
}