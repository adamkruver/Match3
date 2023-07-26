using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformScaleBindable : BindableViewProperty<Vector3>
    {
        public override Vector3 BindableProperty
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
    }
}