using Kruver.Mvvm.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Kruver.Mvvm.ViewBindings.Images
{
    public class ImageSpriteBindable : BindableViewProperty<Sprite>
    {
        [SerializeField] private Image _image;

        public override Sprite BindableProperty
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }
    }
}