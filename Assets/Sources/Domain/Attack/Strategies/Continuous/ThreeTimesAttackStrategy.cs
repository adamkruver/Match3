namespace Match3.Domain.Attack.Strategies.Continuous
{
    public class ThreeTimesAttackStrategy : ContinuousAttackStrategy
    {
        public ThreeTimesAttackStrategy() :base()
        {
        }

        private ThreeTimesAttackStrategy(AttackInfo permanent) : base(permanent)
        {
        }

        public override IAttackStrategy Clone(AttackInfo attackInfo)
        {
            return new ThreeTimesAttackStrategy(attackInfo);
        }

        protected override bool TryGetNext(int counter, out AttackInfo attackInfo)
        {
            attackInfo = Permanent;

            return counter != 3;
        }
    }
}