namespace Match3.Domain.Attack.Strategies
{
    public interface ICloneableAttackStrategy : IAttackStrategy
    {
        IAttackStrategy Clone(AttackInfo attackInfo);
    }
}