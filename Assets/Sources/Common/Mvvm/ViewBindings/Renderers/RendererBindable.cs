using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Renderers
{
    public class RendererBindable : BindableViewProperty<Renderer>
    {
        [SerializeField] private Renderer _renderer;

        public override Renderer BindableProperty
        {
            get => _renderer;
            set { return; }
        }
    }
}