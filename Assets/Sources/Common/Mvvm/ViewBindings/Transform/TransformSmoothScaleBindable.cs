using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformSmoothScaleBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private ChangeTransformScaleBindable _changeTransformScaleBindable;

        private Vector3 _startScale;

        private void Awake() => 
            _startScale = transform.localScale;

        private void OnEnable() => 
            transform.localScale = _startScale;

        public override Vector3 BindableProperty
        {
            get => transform.localScale;
            set => _changeTransformScaleBindable?.SetScale(value);
        }
    }
}