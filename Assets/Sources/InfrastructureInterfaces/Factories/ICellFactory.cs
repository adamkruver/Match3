using Match3.Domain;

namespace Sources.InfrastructureInterfaces.Factories
{
    public interface ICellFactory
    {
        Cell Create(ICellType type, int x, int y);
    }
}