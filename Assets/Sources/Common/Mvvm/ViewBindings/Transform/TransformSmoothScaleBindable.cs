using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformSmoothScaleBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private ChangeTransformScaleBindable _changeTransformScaleBindable;
        
        public override Vector3 BindableProperty
        {
            get => transform.localScale;
            set => _changeTransformScaleBindable?.SetScale(value);
        }
    }
}