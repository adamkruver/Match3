using System.Collections.Generic;
using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Damain;
using Match3.Damain.Types;
using Sources.Controllers.Cells;
using Sources.Presentation.Views;
using UnityEngine;

namespace Match3.Application
{
    public class Game
    {
        private readonly ViewFactory _viewFactory;

        public Game(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Run()
        {
            List<Cell> cells = new List<Cell>();
            
            for (int x = 0; x < 1; x++)
            {
                for (int y = 0; y < 1; y++)
                {
                    cells.Add(new Cell(new Orange(), new Vector2Int(x, y)));

                }
            }

            foreach (Cell cell in cells)
            {
                CellViewModel cellViewModel = new CellViewModel(cell);
                IBindableView view = _viewFactory.Create(typeof(OrangeView), @"Views/Cells/");
            
                view.Bind(cellViewModel);
            }
        }

        public void Update()
        {
        }

        public void Finish()
        {
        }
    }
}