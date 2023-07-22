using System;
using Match3.Damain;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class SelectableService : ISelectableService
    {
        private Cell _primary;
        private Cell _secondary;

        public void Select(Cell cell)
        {
            if (_primary == null)
            {
                _primary = cell;
                _primary.Select();
                return;
            }

            if (_primary == cell)
            {
                _primary.Unselect();
                _primary = null;
                return;
            }

            _secondary = cell;
            _secondary.Select();
            AdjacencyCheck();
        }

        private void AdjacencyCheck()
        {
            int modX = Math.Abs(_primary.Position.x - _secondary.Position.x);
            int modY = Math.Abs(_primary.Position.y - _secondary.Position.y);
            int mod = modX + modY;

            if (mod != 1)
            {
                _primary.Unselect();
                _primary = _secondary;
                _secondary = null;
                return;
            }

            SwitchPosition();

            _secondary.Unselect();
            _primary.Unselect();
            _secondary = null;
            _primary = null;
        }

        private void SwitchPosition()
        {
            Vector2Int position = _primary.Position;
            _primary.SetPosition(_secondary.Position);
            _secondary.SetPosition(position);
        }
    }
}