using System;
using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Match3.Presentation.Sources.Presentation.Factories;
using Sources.Controllers.Cells;
using Sources.Infrastructure.Factories;
using Sources.Infrastructure.Services;

namespace Match3.Application
{
    public class Game
    {
        private readonly TableFactory _tableFactory;

        public Game(
            TableFactory tableFactory
        )
        {
            _tableFactory = tableFactory;
        }

        public void Run()
        {
            _tableFactory.Create(8, 8);
        }

        public void Update()
        {
        }

        public void Finish()
        {
        }
    }
}