using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.RectTransforms
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPositionBindable : BindableViewProperty<Vector3>
    {
        private RectTransform _rectTransform;

        private Vector3 _position;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        private void Update() =>
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _position, Time.deltaTime * 100);

        public override Vector3 BindableProperty
        {
            get => _rectTransform.position;
            set => _position = value;
        }
    }
}