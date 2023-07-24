using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Match3.Domain;
using Match3.Domain.Sources.Domain.Tables;
using Sources.InfrastructureInterfaces.Services;

namespace Sources.Infrastructure.Services
{
    public class SelectableService : ISelectableService
    {
        private readonly ITableService _tableService;
        private readonly Table _table;

        private Cell _primary;
        private Cell _secondary;
        private List<Cell> _cells;

        private bool _isEnabled = true;

        public SelectableService(ITableService tableService, Table table)
        {
            _tableService = tableService;
            _table = table;
        }

        public void Enable()
        {
            if (_isEnabled)
                return;

            _isEnabled = true;
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;

            _isEnabled = false;
        }

        public void Select(Cell cell)
        {
            if (_isEnabled == false)
                return;
            
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

        private async UniTask AdjacencyCheck()
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

            Disable();
            
            await SwitchPosition();

            _secondary.Unselect();
            _primary.Unselect();
            _secondary = null;
            _primary = null;
            
            while (await _tableService.TryGetMatches())
            {
            }
            
            Enable();
        }

        private async UniTask SwitchPosition()
        {
            _table.Switch(_primary, _secondary);

            _cells = new List<Cell>(new List<Cell>()
            {
                _primary,
                _secondary
            });

            _primary.PositionChanged += RemoveFromList;
            _secondary.PositionChanged += RemoveFromList;

            while (_cells.Count != 0)
            {
                await UniTask.NextFrame();
            }
        }

        private void RemoveFromList(Cell cell)
        {
            cell.PositionChanged -= RemoveFromList;
            _cells.Remove(cell);
        }
    }
}