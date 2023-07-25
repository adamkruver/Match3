using System.Collections.Generic;
using Codice.Client.Commands.Xlinks;
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
            new Orange(),
            new Garlic()
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

                    newCells.Add(CreateCell(cellType, x, y));
                }
            }

            return newCells;
        }

        private Cell CreateCell(ICellType cellType, int x, int y)
        {
            _table[x, y] = _cellFactory.Create(cellType, x, y);
            Cell cell = _table[x, y];

            CellViewModel cellViewModel = new CellViewModel(cell, _selectableService);
            IBindableView view = _cellViewBuilder.Build(cell.CellType);

            view.Bind(cellViewModel);

            return cell;
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
            long[,] cells = new long[_table.Width, _table.Height];

            GetVerticalMatches(cells);
            GetHorizontalMatches(cells);

            _cellsToDestroy = GetMatchedCells(cells);

            if (_cellsToDestroy.Count == 0)
                return false;

            await DestroyCellsAsync(cells);
            DropDown();
            await FillAsync();

            return true;
        }

        private async UniTask DestroyCellsAsync(long[,] cells)
        {
            for (int i = _cellsToDestroy.Count - 1; i >= 0; i--)
            {
                Cell cell = _cellsToDestroy[i];

                int x = cell.Position.x;
                int y = cell.Position.y;
                ICellType cellType = cell.CellType;

                long width = (cells[x, y] & cellType.Mask) >> cellType.Offset;
                Debug.Log($"{cellType.GetType().Name} : {width}");

                if (width <= 1)
                {
                    _table.Destroy(x, y);
                    cell.Destroyed += OnCellDestroyed;

                    continue;
                }

                _cellsToDestroy.Remove(cell);
                cell.NotifyDestroyed();
                CreateCell(new Multi(), x, y);
            }

            while (_cellsToDestroy.Count > 0)
                await UniTask.NextFrame();
        }

        private async UniTask FillAsync()
        {
            _newCells = Fill();

            foreach (Cell cell in _newCells)
                cell.PositionChanged += OnPositionChanged;

            while (_newCells.Count > 0)
                await UniTask.NextFrame();
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

        private List<Cell> GetMatchedCells(long[,] cells)
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

        private void GetHorizontalMatches(long[,] cells)
        {
            for (int y = 0; y < _table.Height; y++)
            {
                for (int x = 0; x < _table.Width; x++)
                {
                    (int repeats, int nextX) = GetHorizontalRepeats(x, y);

                    CalculateHorizontalWeight(cells, y, x, x + repeats - 1);
                    x = nextX;
                }
            }
        }

        private void GetVerticalMatches(long[,] cells)
        {
            for (int x = 0; x < _table.Width; x++)
            {
                for (int y = 0; y < _table.Height; y++)
                {
                    (int repeats, int nextY) = GetVerticalRepeats(x, y);

                    CalculateVerticalWeight(cells, x, y, y + repeats - 1);
                    y = nextY;
                }
            }
        }

        private (int repeats, int nextY) GetVerticalRepeats(int x, int y)
        {
            int nextY = y;
            int repeats = 1;

            ICellType currentCellType = GetNextVerticalCellType(x, y);

            if (currentCellType == null)
            {
                return (1, _table.Height);
            }

            for (int i = y + 1; i < _table.Height; i++)
            {
                ICellType cellType = _table[x, i].CellType;

                if (cellType is Multi)
                {
                    repeats++;
                    continue;
                }

                if (cellType.GetType() == currentCellType.GetType())
                {
                    repeats++;
                    nextY = i;
                    continue;
                }

                break;
            }

            return (repeats, nextY);
        }

        private (int repeats, int nextX) GetHorizontalRepeats(int x, int y)
        {
            int nextX = x;
            int repeats = 1;

            ICellType currentCellType = GetNextHorizontalCellType(x, y);

            if (currentCellType == null)
            {
                return (1, _table.Width);
            }

            for (int i = x + 1; i < _table.Width; i++)
            {
                ICellType cellType = _table[i, y].CellType;

                if (cellType is Multi)
                {
                    repeats++;
                    continue;
                }

                if (cellType.GetType() == currentCellType.GetType())
                {
                    repeats++;
                    nextX = i;
                    continue;
                }

                break;
            }

            return (repeats, nextX);
        }

        private ICellType GetNextVerticalCellType(int x, int y)
        {
            for (int i = y; i < _table.Height; i++)
            {
                ICellType cellType = _table[x, i].CellType;

                if (cellType is not Multi)
                    return cellType;
            }

            return null;
        }

        private ICellType GetNextHorizontalCellType(int x, int y)
        {
            for (int i = x; i < _table.Width; i++)
            {
                ICellType cellType = _table[i, y].CellType;

                if (cellType is not Multi)
                    return cellType;
            }

            return null;
        }

        private void CalculateHorizontalWeight(
            long[,] cells,
            int y,
            int startX,
            int endX
        )
        {
            int matches = endX - startX + 1;

            ICellType currentCellType = GetNextHorizontalCellType(startX, y);

            if (currentCellType == null)
                return;

            if (matches >= 3)
            {
                for (int i = startX; i <= endX; i++)
                    cells[i, y] += _table[i, y].CellType.Weight;
            }

            if (matches >= 4)
            {
                for (int i = 0; i < startX; i++)
                {
                    ICellType cellType = _table[i, y].CellType;

                    long currentWeight = (cells[i, y] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[i, y] = cellType.Weight;
                }

                for (int i = endX + 1; i < _table.Width; i++)
                {
                    ICellType cellType = _table[i, y].CellType;

                    long currentWeight = (cells[i, y] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[i, y] = cellType.Weight;
                }
            }

            if (matches >= 5)
            {
                int index = Mathf.CeilToInt((endX - startX) / 2f) + startX;
                cells[index, y] += currentCellType.Weight;
            }
        }

        private void CalculateVerticalWeight(
            long[,] cells,
            int x,
            int startY,
            int endY)
        {
            int matches = endY - startY + 1;

            ICellType currentCellType = GetNextVerticalCellType(x, startY);

            if (currentCellType == null)
                return;

            if (matches >= 3)
            {
                for (int i = startY; i <= endY; i++)
                    cells[x, i] += _table[x, i].CellType.Weight;
            }

            if (matches >= 4)
            {
                for (int i = 0; i < startY; i++)
                {
                    ICellType cellType = _table[x, i].CellType;

                    long currentWeight = (cells[x, i] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[x, i] = cellType.Weight;
                }

                for (int i = endY + 1; i < _table.Height; i++)
                {
                    ICellType cellType = _table[x, i].CellType;

                    long currentWeight = (cells[x, i] & cellType.Mask) >> cellType.Offset;

                    if (currentWeight == 0)
                        cells[x, i] = cellType.Weight;
                }
            }

            if (matches >= 5)
            {
                int index = Mathf.CeilToInt((endY - startY) / 2f) + startY;
                cells[x, index] += currentCellType.Weight;
            }
        }
    }
}