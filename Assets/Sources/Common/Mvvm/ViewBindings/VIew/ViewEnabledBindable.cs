using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Views;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.GameObject
{
    public class ViewEnabledBindable : BindableViewProperty<bool>
    {
        [SerializeField] private BindableView _view;

        public override bool BindableProperty
        {
            get => _view.gameObject.activeSelf;
            set
            {
                if (value)
                    _view.Show();
                else
                    _view.Hide();
            }
        }
    }
}