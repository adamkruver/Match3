using UnityEngine;

namespace Match3.Presentation.Assets.Sources.Presentation.Views.HitPoints
{
    public class HitPointsView : MonoBehaviour
    {
        public void Add(HitPointsBarView hitPointsBarView)
        {
            hitPointsBarView.transform.SetParent(transform, false);
        }
    }
}
