using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Views;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.GameObject
{
    public class ViewBindable : BindableViewProperty<BindableView>
    {
        [SerializeField] private BindableView _view;
        
        public override BindableView BindableProperty
        {
            get => _view;
            set => _view = value;
        }
    }
}