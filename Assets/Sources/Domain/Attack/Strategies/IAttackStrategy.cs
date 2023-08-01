namespace Match3.Domain.Attack.Strategies
{
    public interface IAttackStrategy
    {
        AttackInfo Permanent { get; }

        bool TryGetNext(out AttackInfo attackInfo);
    }
}
