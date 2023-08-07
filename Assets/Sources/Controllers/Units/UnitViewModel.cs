using Kruver.Mvvm.Methods.Attributes;
using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.Buttons;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.GameObjects;
using Kruver.Mvvm.ViewModels;
using Match3.Common.Assets.Sources.Common.Mvvm.ViewBindings.Selectable;
using Match3.Domain.Assets.Sources.Domain.Players;
using Match3.Domain.Assets.Sources.Domain.Units.Components;
using Match3.Domain.Units;
using System;
using UnityEngine;

namespace Match3.Controllers.Assets.Sources.Controllers.Units
{
    public class UnitViewModel : ViewModel<IUnit>
    {
        private readonly Player _player;
        private readonly SelectableComponent _selectable;

        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;
        
        [PropertyBinding(typeof(GameObjectEnabledBindable))]
        private ObservableProperty<bool> _isSelected;

        public UnitViewModel(IUnit model, Player player) : base(model)
        {
            _player = player;
            _selectable = model.Get<SelectableComponent>();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            _selectable.Changed -= OnSelectionChanged;
        }

        protected override void OnEnable()
        {
            _isEnabled.Set(true);
            _selectable.Changed += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            _isSelected.Set(_selectable.IsSelected);
        }

        [MethodBinding(typeof(ButtonClickBindable))]
        private void OnClick(Vector3 position)
        {
            _player.Select(Model);
        }
    }
}
