using Kruver.Mvvm.Methods.Attributes;
using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewBindings.Click;
using Kruver.Mvvm.ViewBindings.GameObject;
using Kruver.Mvvm.ViewBindings.Transform;
using Kruver.Mvvm.ViewModels;
using Match3.Damain;
using UnityEngine;

namespace Sources.Controllers.Cells
{
    public class CellViewModel : ViewModel<Cell>
    {
        [PropertyBinding(typeof(ViewEnabledBindable))]
        private ObservableProperty<bool> _isEnabled;
        
        [PropertyBinding(typeof(TransformPositionBindable))]
        private ObservableProperty<Vector3> _position;

        public CellViewModel(Cell model) : base(model)
        {
        }

        protected override void OnEnable()
        {
            _isEnabled.Set(true);
            OnPositionChanged();
            
            AddListeners();
        }

        protected override void OnDisable()
        {
            _isEnabled.Set(false);
            RemoveListeners();
        }

        private void AddListeners()
        {
            Model.PositionChanged += OnPositionChanged;
        }

        private void RemoveListeners()
        {
            Model.PositionChanged -= OnPositionChanged;
        }

        [MethodBinding(typeof(ClickBindable))]
        private void BindClick(Vector3 position)
        {
            System.Random random = new System.Random();

            int positionX = Model.Position.x;
            int positionY = Model.Position.y;
            
            if (random.Next(2) == 0)
                positionX += random.Next(2) == 0 ? 1 : -1;
            else
                positionY += random.Next(2) == 0 ? 1 : -1;
            
            Model.SetPosition(new Vector2Int(positionX, positionY));
        }

        private void OnPositionChanged()
        {
            int positionMultiplier = 1;
            _position.Set(new Vector3(Model.Position.x * positionMultiplier, Model.Position.y * positionMultiplier, 0));
        }
    }
}