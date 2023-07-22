using System;
using UnityEngine;

namespace Match3.Damain
{
    public class Cell
    {
        public Cell(ICellType cellType, Vector2Int position)
        {
            CellType = cellType;
            Position = position;
        }

        public event Action PositionChanged;

        public ICellType CellType { get; }
        public Vector2Int Position { get; private set; }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
            PositionChanged?.Invoke();
        }
    }
}