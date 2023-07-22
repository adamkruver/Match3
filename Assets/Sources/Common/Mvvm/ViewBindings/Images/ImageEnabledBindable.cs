using Kruver.Mvvm.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Kruver.Mvvm.ViewBindings.Images
{
    public class ImageEnabledBindable : BindableViewProperty<bool>
    {
        [SerializeField] private Image _image;

        public override bool BindableProperty
        {
            get => _image.enabled;
            set => _image.enabled = value;
        }
    }
}