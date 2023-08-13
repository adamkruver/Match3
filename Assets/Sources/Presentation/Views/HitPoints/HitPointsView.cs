using Kruver.Mvvm.Views;

namespace Match3.Presentation.Assets.Sources.Presentation.Views.HitPoints
{
    public class HitPointsView : BindableView
    {
        public void Add(HitPointsBarView hitPointsBarView)
        {
            hitPointsBarView.transform.SetParent(transform, false);
        }

        private void Awake()
        {
            Construct(new Kruver.Mvvm.Binder());
        }
    }
}
