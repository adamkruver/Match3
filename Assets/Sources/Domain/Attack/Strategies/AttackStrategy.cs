namespace Match3.Domain.Attack.Strategies
{
    public class AttackStrategy : ICloneableAttackStrategy
    {
        public AttackStrategy()
        {
        }

        private AttackStrategy(AttackInfo attackInfo)
        {
            Permanent = attackInfo;
        }

        public AttackInfo Permanent { get; }

        public IAttackStrategy Clone(AttackInfo attackInfo)
        {
            return new AttackStrategy(attackInfo);
        }

        public bool TryGetNext(out AttackInfo attackInfo)
        {
            attackInfo = null;

            return false;
        }
    }
}
