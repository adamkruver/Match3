using System;
using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Domain;
using Match3.PresentationInterfaces.Factories;
using Match3.PresentationInterfaces.Sources.PresentationInterfaces.Builders;
using Sources.Infrastructure.ObjectPools;

namespace Match3.Presentation.Builders
{
    public class CellViewBuilder : ICellViewBuilder
    {
        private readonly IViewTypeFactory _viewTypeFactory;
        private readonly ObjectPool<BindableView> _objectPool;
        private readonly ViewFactory _viewFactory;

        public CellViewBuilder(
            IViewTypeFactory viewTypeFactory,
            ObjectPool<BindableView> objectPool,
            ViewFactory viewFactory
        )
        {
            _viewTypeFactory = viewTypeFactory;
            _objectPool = objectPool;
            _viewFactory = viewFactory;
        }

        public IBindableView Build(ICellType cellType)
        {
            Type viewType = _viewTypeFactory.Create(cellType.GetType());
            
            return _objectPool.Get(viewType) ?? _viewFactory.Create(viewType, @"Views/Cells/");
        }
    }
}