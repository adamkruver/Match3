using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformPositionBindable : BindableViewProperty<Vector3>
    {
        public override Vector3 BindableProperty
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}