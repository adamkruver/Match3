using Kruver.Mvvm.Properties;
using TMPro;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.TextMeshPros
{
    public class TextMeshProTextBindable : BindableViewProperty<string>
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        public override string BindableProperty
        {
            get => _textMesh.text; 
            set => _textMesh.text = value;
        }
    }
}