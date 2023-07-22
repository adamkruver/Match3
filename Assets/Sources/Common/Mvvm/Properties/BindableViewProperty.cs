using Kruver.Mvvm.Properties.Factories;
using UnityEngine;

namespace Kruver.Mvvm.Properties
{
    public abstract class BindableViewProperty<T> : MonoBehaviour, IBindableViewProperty<T>
    {
        private ObservableProperty<T> _observableProperty;

        public ObservableProperty<T> GetBinding()
        {
            _observableProperty = new ObservablePropertyFactory().Create<T>(this, nameof(BindableProperty));
            return _observableProperty;
        }

        public abstract T BindableProperty { get; set; }
    }
}