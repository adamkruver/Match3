using Match3.Domain;
using Sources.InfrastructureInterfaces.Factories;
using UnityEngine;

namespace Sources.Infrastructure.Factories
{
    public class CellFactory : ICellFactory
    {
        public Cell Create(ICellType type, int x, int y) => 
            new Cell(type, new Vector2Int(x, y));
    }
}