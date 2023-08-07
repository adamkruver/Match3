using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewModels;
using Match3.Common.Assets.Sources.Common.Mvvm.ViewBindings.RectTransforms;
using Match3.Domain.Assets.Sources.Domain.Players;
using Match3.Domain.Assets.Sources.Domain.Units.Components;
using System;
using UnityEngine;

namespace Match3.Controllers.Assets.Sources.Controllers.HitPoints
{
    public class HitPointsViewModel : ViewModel<Player>
    {
        [PropertyBinding(typeof(RectTransformPositionProviderBindable))]
        private ObservableProperty<Vector2[]> _positions;

        public HitPointsViewModel(Player model) : base(model)
        {
        }

        protected override void OnDisable()
        {
            Model.SelectChanged -= OnSelectChanged;
        }

        protected override void OnEnable()
        {
            Model.SelectChanged += OnSelectChanged;
        }

        private void OnSelectChanged()
        {
            Model.Selected.Get<BarPositionComponent>().Position = _positions.Value[0];

            for (int i = 0; i < Model.Unselecteds.Length; i++)
            {
                BarPositionComponent component = Model.Unselecteds[i].Get<BarPositionComponent>();
                component.Position = _positions.Value[i + 1];
            }
        }
    }
}
