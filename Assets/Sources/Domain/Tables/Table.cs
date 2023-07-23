using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3.Domain.Sources.Domain.Tables
{
    public class Table : IEnumerable<Cell>
    {
        private readonly List<Cell> _cells;

        public Table(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new List<Cell>(Enumerable.Repeat<Cell>(null, width * height));
        }

        public Cell this[int x, int y]
        {
            get => _cells[x * Height + y];
            set => _cells[x * Height + y] = value;
        }

        public int Height { get; }
        public int Width { get; }

        public IEnumerator<Cell> GetEnumerator() =>
            _cells.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public void Destroy(int x, int y)
        {
            this[x, y].Destroy();
            this[x, y] = null;
        }

        public void Switch(Cell cell1, Cell cell2) =>
            Switch(cell1.Position.x, cell1.Position.y, cell2.Position.x, cell2.Position.y);

        public void Switch(int x1, int y1, int x2, int y2)
        {
            Cell cell1 = this[x1, y1];
            Cell cell2 = this[x2, y2];

            Vector2Int position1 = new Vector2Int(x1, y1);
            Vector2Int position2 = new Vector2Int(x2, y2);

            cell1?.SetPosition(position2);
            cell2?.SetPosition(position1);

            this[x1, y1] = cell2;
            this[x2, y2] = cell1;
        }
    }
}