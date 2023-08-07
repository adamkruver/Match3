using Kruver.Mvvm.Views;
using Match3.Presentation.Assets.Sources.Presentation.Views.HitPoints;
using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Views.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform[] _parents;

        [field: SerializeField] public HitPointsView HitPointsView { get; private set; }

        private int _counter = 0;

        public void AddChild(BindableView view, HitPointsBarView hitPointsBarView)
        {
            view.transform.SetParent(_parents[_counter], false);
            HitPointsView.Add(hitPointsBarView);
            _counter++;
        }
    }
}