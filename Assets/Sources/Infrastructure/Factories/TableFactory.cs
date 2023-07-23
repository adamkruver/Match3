using Match3.Domain.Sources.Domain.Tables;
using Match3.PresentationInterfaces.Sources.PresentationInterfaces.Builders;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factories;

namespace Sources.Infrastructure.Factories
{
    public class TableFactory : ITableFactory
    {
        private readonly ICellViewBuilder _cellViewBuilder;
        private readonly ICellFactory _cellFactory;

        public TableFactory(
            ICellFactory cellFactory,
            ICellViewBuilder cellViewBuilder
        )
        {
            _cellViewBuilder = cellViewBuilder;
            _cellFactory = cellFactory;
        }

        public Table Create(int width, int height)
        {
            Table table = new Table(width, height);

            TableService tableService =
                new TableService(table, _cellFactory, _cellViewBuilder);

            tableService.Fill();

            return table;
        }
    }
}