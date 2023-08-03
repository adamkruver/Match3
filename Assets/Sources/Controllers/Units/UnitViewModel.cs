using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewModels;
using Match3.Domain.Units;

namespace Match3.Controllers.Assets.Sources.Controllers.Units
{
    public class UnitViewModel : ViewModel<IUnit>
    {
        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;

        public UnitViewModel(IUnit model) : base(model)
        {
        }

        protected override void OnDisable()
        {
            
        }

        protected override void OnEnable()
        {
            _isEnabled.Set(true);
        }
    }
}
