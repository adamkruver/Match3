using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.Sliders;
using Kruver.Mvvm.ViewBindings.TextMeshPros;
using Kruver.Mvvm.ViewModels;
using Match3.Common.Assets.Sources.Common.Mvvm.ViewBindings.Selectable;
using Match3.Domain.Assets.Sources.Domain.Units.Components;
using Match3.Domain.Units;
using Match3.Domain.Units.Components;
using System;

namespace Match3.Controllers.Assets.Sources.Controllers.HitPoints
{
    public class HitPointsBarViewModel : ViewModel<IUnit>
    {
        [PropertyBinding(typeof(SliderValueBindable))]
        private ObservableProperty<float> _hitPointsSlider;

        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;

        [PropertyBinding(typeof(TextMeshProTextBindable))]
        private ObservableProperty<string> _hitPointsValue;

        [PropertyBinding(typeof(SelectEventBindable))]
        private ObservableProperty<bool> _isSelected;

        private HitPointsComponent _hitPoints;
        private SelectableComponent _selectable;

        public HitPointsBarViewModel(IUnit model) : base(model)
        {
            _hitPoints = Model.Get<HitPointsComponent>();
            _selectable = Model.Get<SelectableComponent>();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            _hitPoints.Changed -= OnHitPointsChanged;
            _selectable.Changed -= OnSelectionChanged;
        }

        protected override void OnEnable()
        {
            _hitPoints.Changed += OnHitPointsChanged;
            _selectable.Changed += OnSelectionChanged;
            OnHitPointsChanged();
            _isEnabled.Set(true);
        }

        private void OnSelectionChanged()
        {
            _isSelected.Set(_selectable.IsSelected);
        }

        private void OnHitPointsChanged()
        {
            _hitPointsSlider.Set(_hitPoints.Normalized);
            _hitPointsValue.Set(_hitPoints.Current.ToString());
        }
    }
}
