using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformUpBindable : BindableViewProperty<Vector3>
    {
        public override Vector3 BindableProperty
        {
            get => transform.up;
            set => transform.up = value;
        }
    }
}