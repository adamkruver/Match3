﻿using Kruver.Mvvm.Methods.Attributes;
using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.Click;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.GameObjects;
using Kruver.Mvvm.ViewBindings.Transform;
using Kruver.Mvvm.ViewModels;
using Match3.Domain;
using Sources.InfrastructureInterfaces.Services;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Sources.Controllers.Cells
{
    public class CellViewModel : ViewModel<Cell>
    {
        private readonly ISelectableService _selectableService;

        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;
        
        [PropertyBinding(typeof(TransformPositionBindable))]
        private ObservableProperty<Vector3> _position;

        [PropertyBinding(typeof(GameObjectEnabledBindable))]
        private ObservableProperty<bool> _isSelected;

        public CellViewModel(
            Cell model,
            ISelectableService selectableService
            ) : base(model)
        {
            _selectableService = selectableService;
        }

        protected override void OnEnable()
        {
            _isEnabled.Set(true);
            OnPositionChanged();
            
            AddListeners();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            RemoveListeners();
        }

        private void AddListeners()
        {
            Model.PositionChanged += OnPositionChanged;
            Model.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            _isSelected.Set(Model.IsSelected);
        }

        private void RemoveListeners()
        {
            Model.PositionChanged -= OnPositionChanged;
            Model.SelectionChanged -= OnSelectionChanged;
        }

        [MethodBinding(typeof(ClickBindable))]
        private void BindClick(Vector3 position) =>
            _selectableService.Select(Model);

        private void OnPositionChanged()
        {
            int positionMultiplier = 1;
            _position.Set(new Vector3(Model.Position.x * positionMultiplier, Model.Position.y * positionMultiplier, 0));
        }
    }
}