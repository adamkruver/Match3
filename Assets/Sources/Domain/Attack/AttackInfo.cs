namespace Match3.Domain.Attack
{
    public class AttackInfo
    {
        public AttackInfo(float damage, int rating, float offence, int luck)
        {
            Damage = damage;
            Rating = rating;
            Offence = offence;
            Luck = luck;
        }

        public float Damage { get; }
        public int Rating { get; }
        public float Offence { get; }
        public int Luck { get; }
    }
}
