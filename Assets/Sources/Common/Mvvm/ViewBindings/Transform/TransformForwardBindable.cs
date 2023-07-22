using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformForwardBindable : BindableViewProperty<Vector3>
    {
        public override Vector3 BindableProperty
        {
            get => transform.forward;
            set => transform.forward = value;
        }
    }
}