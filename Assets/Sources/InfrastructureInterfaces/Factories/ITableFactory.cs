using Match3.Domain.Sources.Domain.Tables;

namespace Sources.InfrastructureInterfaces.Factories
{
    public interface ITableFactory
    {
        Table Create(int width, int height);
    }
}