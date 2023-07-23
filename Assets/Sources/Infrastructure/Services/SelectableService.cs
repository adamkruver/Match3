using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public SelectableService(ITableService tableService, Table table)
        {
            _tableService = tableService;
            _table = table;
        }

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

            await SwitchPosition();

            while (_tableService.TryGetMatches(out Cell[] cells))
            {
                // TODO:  GET REWARD FROM Cells
            }

            _secondary.Unselect();
            _primary.Unselect();
            _secondary = null;
            _primary = null;
        }

        private async UniTask SwitchPosition()
        {
            _table.Switch(_primary, _secondary);

            _cells = new List<Cell>(new List<Cell>()
            {
                _primary,
                _secondary
            });

            _primary.PositionChanged += RemoveFromStack;
            _secondary.PositionChanged += RemoveFromStack;

            while (_cells.Count != 0)
            {
                await UniTask.NextFrame();
            }
        }

        private void RemoveFromStack(Cell cell)
        {
            cell.PositionChanged -= RemoveFromStack;
            _cells.Remove(cell);
        }
    }
}