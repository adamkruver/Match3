using Kruver.Mvvm.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Kruver.Mvvm.ViewBindings.Sliders
{
    public class SliderValueBindable : BindableViewProperty<float>
    {
        [SerializeField] private Slider _slider;

        public override float BindableProperty
        {
            get => _slider.value;
            set => _slider.value = value;
        }
    }
}