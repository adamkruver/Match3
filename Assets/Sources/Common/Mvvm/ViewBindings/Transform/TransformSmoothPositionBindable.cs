using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformSmoothPositionBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private ChangeTransformPositionBindable _changeTransformPositionBindable;

        public override Vector3 BindableProperty
        {
            get => transform.position;
            set => _changeTransformPositionBindable?.SetPosition(value);
        }
    }
}