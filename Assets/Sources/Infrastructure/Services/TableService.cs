using System;
using System.Collections.Generic;
using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Match3.Domain.Types;
using Match3.PresentationInterfaces.Factories;
using Sources.Controllers.Cells;
using Sources.InfrastructureInterfaces.Factories;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;
using Random = System.Random;

namespace Sources.Infrastructure.Services
{
    public class TableService : ITableService
    {
        private readonly Table _table;
        private readonly ICellFactory _cellFactory;
        private readonly IViewTypeFactory _viewTypeFactory;
        private readonly ISelectableService _selectableService;
        private readonly ViewFactory _viewFactory;
        private readonly Random _random = new Random();

        private readonly ICellType[] _celltypes = new ICellType[]
        {
            new Banana(),
            new Apple(),
            new Grape(),
            new Blueberry(),
            new Tomato(),
            new Orange()
        };

        public TableService(
            Table table,
            ICellFactory cellFactory,
            IViewTypeFactory viewTypeFactory,
            ViewFactory viewFactory
        )
        {
            _table = table;
            _cellFactory = cellFactory;
            _viewTypeFactory = viewTypeFactory;
            _viewFactory = viewFactory;

            _selectableService = new SelectableService(this, table);
        }

        public void Fill()
        {
            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = _table.Height - 1; y >= 0; y--)
                {
                    if (_table[x, y] != null)
                        continue;

                    ICellType cellType = _celltypes[_random.Next(_celltypes.Length)];

                    _table[x, y] = _cellFactory.Create(cellType, x, y);
                    Cell cell = _table[x, y];

                    CellViewModel cellViewModel = new CellViewModel(cell, _selectableService);
                    Type type = _viewTypeFactory.Create(cell.CellType.GetType());
                    IBindableView view = _viewFactory.Create(type, @"Views/Cells/");

                    view.Bind(cellViewModel);
                }
            }
        }

        public void DropDown()
        {
            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = 0; y < _table.Height; y++)
                {
                    if (_table[x, y] != null)
                        continue;

                    bool hasCell = false;

                    for (int i = y + 1; i < _table.Height; i++)
                    {
                        if (_table[x, i] != null)
                        {
                            _table.Switch(x, y, x, i);
                            hasCell = true;
                            break;
                        }
                    }

                    if (hasCell == false)
                        break;
                }
            }
        }

        public bool TryGetMatches(out Cell[] matchedCells)
        {
            matchedCells = null;

            int[,] cells = new int[_table.Width, _table.Height];

            GetVerticalMatches(cells);
            GetHorizontalMatches(cells);
            GetReward(cells, out matchedCells);
            DropDown();
            Fill();

            return matchedCells.Length != 0;
        }

        private void GetReward(int[,] cells, out Cell[] matchedCells)
        {
            List<Cell> destroyedCells = new List<Cell>();

            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = 0; y < _table.Height; y++)
                {
                    if (cells[x, y] != 0)
                    {
                        destroyedCells.Add(_table[x, y]);
                        _table.Destroy(x, y);
                    }
                }
            }

            matchedCells = destroyedCells.ToArray();
        }

        private void GetHorizontalMatches(int[,] cells)
        {
            for (int y = 0; y < _table.Height; y++)
            {
                int startX = 0;
                int endX = 0;

                ICellType currentCellType = _table[0, y].CellType;

                for (int x = 1; x < _table.Width; x++)
                {
                    if (_table[x, y].CellType == currentCellType)
                    {
                        endX = x;
                        continue;
                    }

                    CalculateHorizontalWeight(cells, currentCellType, y, startX, endX);

                    startX = x;
                    endX = x;
                    currentCellType = _table[x, y].CellType;
                }

                CalculateHorizontalWeight(cells, currentCellType, y, startX, endX);
            }
        }

        private void GetVerticalMatches(int[,] cells)
        {
            for (int x = 0; x < _table.Width; x++)
            {
                int startY = 0;
                int endY = 0;

                ICellType currentCellType = _table[x, 0].CellType;

                for (int y = 1; y < _table.Height; y++)
                {
                    if (_table[x, y].CellType == currentCellType)
                    {
                        endY = y;
                        continue;
                    }

                    CalculateVerticalWeight(cells, currentCellType, x, startY, endY);

                    startY = y;
                    endY = y;
                    currentCellType = _table[x, y].CellType;
                }

                CalculateVerticalWeight(cells, currentCellType, x, startY, endY);
            }
        }

        private void CalculateHorizontalWeight(
            int[,] cells,
            ICellType cellType,
            int y,
            int startX,
            int endX
        )
        {
            int matches = endX - startX + 1;

            if (matches >= 3)
            {
                for (int i = startX; i <= endX; i++)
                    cells[i, y] += cellType.Weight;
            }

            if (matches >= 4)
            {
                for (int i = 0; i < startX; i++)
                {
                    int currentWeight = (cells[i, y] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[i, y] = cellType.Weight;
                }

                for (int i = endX + 1; i < _table.Width; i++)
                {
                    int currentWeight = (cells[i, y] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[i, y] = cellType.Weight;
                }
            }

            if (matches >= 5)
            {
                int index = Mathf.CeilToInt((endX - startX) / 2f) + startX;
                cells[index, y] += cellType.Weight;
            }
        }

        private void CalculateVerticalWeight(
            int[,] cells,
            ICellType cellType,
            int x,
            int startY,
            int endY)
        {
            int matches = endY - startY + 1;

            if (matches >= 3)
            {
                for (int i = startY; i <= endY; i++)
                    cells[x, i] += cellType.Weight;
            }

            if (matches >= 4)
            {
                for (int i = 0; i < startY; i++)
                {
                    int currentWeight = (cells[x, i] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[x, i] = cellType.Weight;
                }

                for (int i = endY + 1; i < _table.Height; i++)
                {
                    int currentWeight = (cells[x, i] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[x, i] = cellType.Weight;
                }
            }

            if (matches >= 5)
            {
                int index = Mathf.CeilToInt((endY - startY) / 2f) + startY;
                cells[x, index] += cellType.Weight;
            }
        }
    }
}