using Kruver.Mvvm.Methods;
using UnityEngine;
using UnityEngine.UI;

namespace Kruver.Mvvm.ViewBindings.Buttons
{
    public class ButtonClickBindable : BindableViewMethod<Vector3>
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            BindingCallback.Invoke(Input.mousePosition);
        }
    }
}