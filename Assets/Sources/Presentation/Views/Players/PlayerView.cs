using Kruver.Mvvm.Views;
using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Views.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform[] _parents;

        private int _counter = 0;

        public void AddChild(BindableView bindableView)
        {
            bindableView.transform.SetParent(_parents[_counter]);
            _counter++;
        }
    }
}