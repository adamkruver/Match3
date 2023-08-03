using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.Sliders;
using Kruver.Mvvm.ViewBindings.TextMeshPros;
using Kruver.Mvvm.ViewModels;
using Match3.Domain.Units;
using Match3.Domain.Units.Components;
using System;

namespace Match3.Controllers.Assets.Sources.Controllers.HitPoints
{
    public class HitPointsViewModel : ViewModel<IUnit>
    {
        [PropertyBinding(typeof(SliderValueBindable))]
        private ObservableProperty<float> _hitPointsSlider;

        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;

        [PropertyBinding(typeof(TextMeshProTextBindable))]
        private ObservableProperty<string> _hitPointsValue;

        private HitPointsComponent _hitPoints;

        public HitPointsViewModel(IUnit model) : base(model)
        {
            _hitPoints = Model.Get<HitPointsComponent>();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            _hitPoints.Changed -= OnHitPointsChanged;
        }

        protected override void OnEnable()
        {
            _hitPoints.Changed += OnHitPointsChanged;
            OnHitPointsChanged();
            _isEnabled.Set(true);
        }

        private void OnHitPointsChanged()
        {
            _hitPointsSlider.Set(_hitPoints.Normalized);
            _hitPointsValue.Set(_hitPoints.Current.ToString());
        }
    }
}
