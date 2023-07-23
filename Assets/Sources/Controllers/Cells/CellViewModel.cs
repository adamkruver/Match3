using Kruver.Mvvm.Methods.Attributes;
using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.Click;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.GameObjects;
using Kruver.Mvvm.ViewBindings.ParticleSystems;
using Kruver.Mvvm.ViewBindings.Transform;
using Kruver.Mvvm.ViewModels;
using Match3.Domain;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Controllers.Cells
{
    public class CellViewModel : ViewModel<Cell>
    {
        private readonly ISelectableService _selectableService;

        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;

        [PropertyBinding(typeof(TransformSmoothPositionBindable))]
        private ObservableProperty<Vector3> _smoothPosition;

        [PropertyBinding(typeof(TransformPositionBindable))]
        private ObservableProperty<Vector3> _startPosition;

        [PropertyBinding(typeof(GameObjectEnabledBindable))]
        private ObservableProperty<bool> _isSelected;

        [PropertyBinding(typeof(TransformSmoothScaleBindable))]
        private ObservableProperty<Vector3> _scale;

        [PropertyBinding(typeof(GameObjectEnabledBindable), "Explosion")]
        private ObservableProperty<bool> _isExplose;

        public CellViewModel(
            Cell model,
            ISelectableService selectableService
        ) : base(model)
        {
            _selectableService = selectableService;
        }

        protected override void OnEnable()
        {
            _isEnabled.Set(true);
            _startPosition.Set(new Vector3(Model.Position.x, Model.Position.y + 8, 0));
            OnPositionChanging();

            AddListeners();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            RemoveListeners();
        }

        private void AddListeners()
        {
            Model.PositionChanging += OnPositionChanging;
            Model.SelectionChanged += OnSelectionChanged;
            Model.Destroying += OnDestroing;
        }

        private void OnDestroing()
        {
            _scale.Set(Vector3.zero);
        }

        private void OnSelectionChanged()
        {
            _isSelected.Set(Model.IsSelected);
        }

        private void RemoveListeners()
        {
            Model.PositionChanging -= OnPositionChanging;
            Model.SelectionChanged -= OnSelectionChanged;
            Model.Destroying += OnDestroing;
        }

        [MethodBinding(typeof(ClickBindable))]
        private void BindClick(Vector3 position) =>
            _selectableService.Select(Model);

        [MethodBinding(typeof(ChangeTransformPositionBindable))]
        private void BindChangePositionCallback(Vector3 position)
        {
            Model.NotifyPositionChanged();
        }

        [MethodBinding(typeof(ChangeTransformScaleBindable))]
        private void BindChangeScaleCallback(Vector3 scale)
        {
            _isExplose.Set(true);
        }

        [MethodBinding(typeof(ParticleSystemAfterPlayBindable))]
        private void BindAfterParticlePlay(bool isEnabled)
        {
            Disable();
        }

        private void OnPositionChanging()
        {
            int positionMultiplier = 1;
            _smoothPosition.Set(new Vector3(Model.Position.x * positionMultiplier,
                Model.Position.y * positionMultiplier, 0));
        }
    }
}