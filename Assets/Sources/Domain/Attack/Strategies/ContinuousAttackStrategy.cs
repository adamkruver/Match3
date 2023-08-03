namespace Match3.Domain.Attack.Strategies
{
    public abstract class ContinuousAttackStrategy : ICloneableAttackStrategy
    {
        private int _counter = 0;

        protected ContinuousAttackStrategy() 
        {
        }

        protected ContinuousAttackStrategy(AttackInfo permanent)
        {
            Permanent = permanent;
        }

        public AttackInfo Permanent { get; }

        public abstract IAttackStrategy Clone(AttackInfo attackInfo);

        public bool TryGetNext(out AttackInfo attackInfo)
        {
            _counter++;

            return TryGetNext(_counter, out attackInfo);
        }

        protected abstract bool TryGetNext(int counter, out AttackInfo attackInfo);
    }
}