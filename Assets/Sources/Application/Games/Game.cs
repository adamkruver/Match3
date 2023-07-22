using System;
using System.Collections.Generic;
using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Match3.Domain.Types;
using Match3.Presentation.Sources.Presentation.Factories;
using Sources.Controllers.Cells;
using Sources.Infrastructure.Services;
using UnityEngine;
using Random = System.Random;

namespace Match3.Application
{
    public class Game
    {
        private readonly ViewFactory _viewFactory;
        private readonly SelectableService _selectableService;
        private readonly ViewTypeFactory _viewTypeFactory;

        public Game(ViewFactory viewFactory, SelectableService selectableService, ViewTypeFactory viewTypeFactory)
        {
            _viewFactory = viewFactory;
            _selectableService = selectableService;
            _viewTypeFactory = viewTypeFactory;
        }

        public void Run()
        {
            Random random = new Random();
            List<Cell> cells = new List<Cell>();
            Table table = new Table(8, 8);
            ICellType[] cellTypes =
            {
                new Orange(),
                new Banana(),
                new Apple(),
                new Blueberry(),
                new Grape(),
                new Tomato()
            };

            for (int x = 0; x < table.Width; x++)
            {
                for (int y = 0; y < table.Height; y++)
                {
                    ICellType type = cellTypes[random.Next(cellTypes.Length)];
                    cells.Add(new Cell(type, new Vector2Int(x, y)));
                }
            }

            foreach (Cell cell in cells)
            {
                CellViewModel cellViewModel = new CellViewModel(cell, _selectableService);
                Type type = _viewTypeFactory.Create(cell.CellType.GetType());
                IBindableView view = _viewFactory.Create(type, @"Views/Cells/");

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