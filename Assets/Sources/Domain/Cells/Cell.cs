using System;
using UnityEngine;

namespace Match3.Domain
{
    public class Cell
    {
        private bool _isSelected;

        public Cell(ICellType cellType, Vector2Int position)
        {
            CellType = cellType;
            Position = position;
        }

        public event Action PositionChanging;
        public event Action<Cell> PositionChanged;
        public event Action SelectionChanged;
        public event Action Destroying;
        public event Action<Cell> Destroyed;

        public ICellType CellType { get; }
        public Vector2Int Position { get; private set; }
        public bool IsSelected { get; private set; }

        public void NotifyPositionChanged() => 
            PositionChanged?.Invoke(this);

        public void NotifyDestroyed() => 
            Destroyed?.Invoke(this);

        public void SetPosition(Vector2Int position)
        {
            Position = position;
            PositionChanging?.Invoke();
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
        
        public void Destroy() => 
            Destroying?.Invoke();
    }
}