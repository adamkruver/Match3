using Match3.Application.Assets.Sources.Application.Games.Builders;
using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Sources.Infrastructure.Factories;

namespace Match3.Application
{
    public class Game
    {
        private readonly TableFactory _tableFactory;
        private readonly PlayerBuilder _playerBuilder;

        public Game(
            TableFactory tableFactory,
            PlayerBuilder playerBuilder
        )
        {
            _tableFactory = tableFactory;
            _playerBuilder = playerBuilder;
        }

        public void Run()
        {
            _tableFactory.Create(8, 8);
            _playerBuilder.BuildLeft(new IUnitType[]
            {
                new Ogre(),
                new Ogre(),
                new Ogre(),
                new Ogre(),
                new Beholder(),
                new Beholder(),
                new Ogre(),
            });

            _playerBuilder.BuildRight(new IUnitType[]
            {
                new Ogre(),
                new Beholder(),
            });
        }

        public void Update()
        {
        }

        public void Finish()
        {
        }
    }
}