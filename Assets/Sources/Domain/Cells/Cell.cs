using System;
using UnityEngine;

namespace Match3.Damain
{
    public class Cell
    {
        private bool _isSelected;

        public Cell(ICellType cellType, Vector2Int position)
        {
            CellType = cellType;
            Position = position;
        }

        public event Action PositionChanged;
        public event Action SelectionChanged;

        public ICellType CellType { get; }
        public Vector2Int Position { get; private set; }
        public bool IsSelected { get; private set; }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
            PositionChanged?.Invoke();
        }

        public void Select()
        {
            if (IsSelected)
                return;

            IsSelected = true;
            SelectionChanged?.Invoke();
        }

        public void Unselect()
        {
            if (IsSelected == false)
                return;

            IsSelected = false;
            SelectionChanged?.Invoke();
        }
    }
}