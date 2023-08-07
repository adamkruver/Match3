using Kruver.Mvvm.Properties;
using System.Linq;
using UnityEngine;

namespace Match3.Common.Assets.Sources.Common.Mvvm.ViewBindings.RectTransforms
{
    public
        class RectTransformPositionProviderBindable : BindableViewProperty<Vector2[]>
    {
        [SerializeField] private RectTransform[] _rectTransforms;

        public override Vector2[] BindableProperty
        { 
            get => _rectTransforms.Select(transform => transform.anchoredPosition).ToArray();
            set { return; }
        }
    }
}
