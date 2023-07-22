using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.RectTransforms
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPivotBindable : BindableViewProperty<Vector2>
    {
        private RectTransform _rectTransform;

        private void Awake() => 
            _rectTransform = GetComponent<RectTransform>();

        public override Vector2 BindableProperty
        {
            get => _rectTransform.pivot;
            set => _rectTransform.pivot = value;
        }
    }
}