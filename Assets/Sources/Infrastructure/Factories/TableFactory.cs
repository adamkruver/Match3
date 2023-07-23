using Kruver.Mvvm.Factories;
using Match3.Domain.Sources.Domain.Tables;
using Match3.PresentationInterfaces.Factories;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factories;
using Sources.InfrastructureInterfaces.Services;

namespace Sources.Infrastructure.Factories
{
    public class TableFactory : ITableFactory
    {
        private readonly ICellFactory _cellFactory;
        private readonly IViewTypeFactory _viewTypeFactory;
        private readonly ViewFactory _viewFactory;

        public TableFactory(
            ICellFactory cellFactory,
            IViewTypeFactory viewTypeFactory,
            ViewFactory viewFactory
        )
        {
            _cellFactory = cellFactory;
            _viewTypeFactory = viewTypeFactory;
            _viewFactory = viewFactory;
        }

        public Table Create(int width, int height)
        {
            Table table = new Table(width, height);

            TableService tableService =
                new TableService(table, _cellFactory, _viewTypeFactory, _viewFactory);

            tableService.Fill();

            return table;
        }
    }
}