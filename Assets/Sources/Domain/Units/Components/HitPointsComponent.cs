using System;
using Match3.Domain.Components;

namespace Match3.Domain.Units.Components
{
    public class HitPointsComponent : IComponent
    {
        public HitPointsComponent(int max)
        {
            Max = max;
            Current = Max;
        }

        public event Action Changed;
        public event Action Died;

        public int Max { get; }
        public int Current { get; private set; }

        public void TakeHit(int points)
        {
            if (Current == 0)
                return;

            if (points <= 0)
                return;

            Current -= points;
            Current = Math.Max(Current , 0);
            Changed?.Invoke();

            if (Current > 0)
                return;

            Died?.Invoke();
        }
    }
}
