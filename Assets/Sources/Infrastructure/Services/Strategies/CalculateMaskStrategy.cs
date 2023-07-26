using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Match3.Domain.Types;
using UnityEngine;

namespace Sources.Infrastructure.Services.Strategies
{
    public class CalculateMaskStrategy
    {
        private readonly Table _table;
        private readonly Vector2Int _horizontal = new Vector2Int(1, 0);
        private readonly Vector2Int _vertical = new Vector2Int(0, 1);

        public CalculateMaskStrategy(Table table)
        {
            _table = table;
        }

        public long[,] Calculate()
        {
            long[,] cells = new long[_table.Width, _table.Height];

            GetVerticalMatches(cells);
            GetHorizontalMatches(cells, _horizontal);
            
            return cells;
        }
        

        private void GetHorizontalMatches(long[,] cells, Vector2Int step)
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