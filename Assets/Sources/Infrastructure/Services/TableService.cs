using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kruver.Mvvm.Views;
using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Match3.Domain.Types;
using Match3.PresentationInterfaces.Sources.PresentationInterfaces.Builders;
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
        private readonly ICellViewBuilder _cellViewBuilder;
        private readonly ISelectableService _selectableService;
        private readonly Random _random = new Random();

        private List<Cell> _cellsToDestroy;
        private List<Cell> _newCells;

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
            ICellViewBuilder cellViewBuilder
        )
        {
            _table = table;
            _cellFactory = cellFactory;
            _cellViewBuilder = cellViewBuilder;
            _selectableService = new SelectableService(this, table);
        }

        public List<Cell> Fill()
        {
            List<Cell> newCells = new List<Cell>();

            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = _table.Height - 1; y >= 0; y--)
                {
                    if (_table[x, y] != null)
                        continue;

                    ICellType cellType = _celltypes[_random.Next(_celltypes.Length)];

                    _table[x, y] = _cellFactory.Create(cellType, x, y);
                    Cell cell = _table[x, y];

                    newCells.Add(cell);

                    CellViewModel cellViewModel = new CellViewModel(cell, _selectableService);
                    IBindableView view = _cellViewBuilder.Build(cell.CellType);

                    view.Bind(cellViewModel);
                }
            }

            return newCells;
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

        public async UniTask<bool> TryGetMatches()
        {
            int[,] cells = new int[_table.Width, _table.Height];

            GetVerticalMatches(cells);
            GetHorizontalMatches(cells);
            _cellsToDestroy = GetMatchedCells(cells);

            if (_cellsToDestroy.Count == 0)
                return false;

            foreach (Cell cell in _cellsToDestroy)
            {
                cell.Destroyed += OnCellDestroyed;
                _table.Destroy(cell.Position.x, cell.Position.y);
            }

            while (_cellsToDestroy.Count > 0)
                await UniTask.NextFrame();

            DropDown();

            _newCells = Fill();

            foreach (Cell cell in _newCells)
                cell.PositionChanged += OnPositionChanged;

            while (_newCells.Count > 0)
                await UniTask.NextFrame();

            return true;
        }

        private void OnCellDestroyed(Cell cell)
        {
            cell.Destroyed -= OnCellDestroyed;
            _cellsToDestroy.Remove(cell);
        }

        private void OnPositionChanged(Cell cell)
        {
            cell.PositionChanged -= OnCellDestroyed;
            _newCells.Remove(cell);
        }

        private List<Cell> GetMatchedCells(int[,] cells)
        {
            List<Cell> destroyedCells = new List<Cell>();

            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = 0; y < _table.Height; y++)
                {
                    if (cells[x, y] != 0)
                    {
                        destroyedCells.Add(_table[x, y]);
                    }
                }
            }

            return destroyedCells;
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