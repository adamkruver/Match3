﻿using Kruver.Mvvm.Factories;
using Kruver.Mvvm.Views;
using Match3.Presentation.Builders;
using Match3.Presentation.Sources.Presentation.Factories;
using Sources.Infrastructure.Factories;
using Sources.Infrastructure.ObjectPools;

namespace Match3.Application.Builders
{
    public class GameBuilder
    {
        public Game Build()
        {
            ViewTypeFactory viewTypeFactory = new ViewTypeFactory();
            ViewFactory viewFactory = new ViewFactory();
            
            // Infrastructure Factories
            CellFactory cellFactory = new CellFactory();

            ObjectPool<BindableView> _viewsPool = new ObjectPool<BindableView>("CellPool");

            CellViewBuilder cellViewBuilder = new CellViewBuilder(viewTypeFactory, _viewsPool, viewFactory);
            
            // Infrastructure Factories
            TableFactory tableFactory = new TableFactory(cellFactory, cellViewBuilder);



            return new Game(tableFactory);
        }
    }
}