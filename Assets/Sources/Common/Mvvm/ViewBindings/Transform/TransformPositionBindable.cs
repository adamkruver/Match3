using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformPositionBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private ChangeTransformPositionBindable _changeTransformPositionBindable;
        [SerializeField] private float _moveSpeed = 20f;

        public override Vector3 BindableProperty
        {
            get => transform.position;
            set
            {
                if (_changeTransformPositionBindable == null)
                {
                    transform.position = value;
                    return;
                }
                
                _changeTransformPositionBindable.SetPosition(value);
            }
        }
    }
}