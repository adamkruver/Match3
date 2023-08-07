using Match3.Domain.Components;
using System;
using UnityEngine;

namespace Match3.Domain.Assets.Sources.Domain.Units.Components
{
    public class PositionComponent : IComponent
    {
        private Vector3 _position;

        public event Action Changed;

        public Vector3 Position
        { 
            get => _position;
            set
            { 
                _position = value;
                Changed?.Invoke();
            } 
        }
    }
}
