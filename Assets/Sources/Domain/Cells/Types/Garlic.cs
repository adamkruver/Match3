namespace Match3.Domain.Types
{
    public class Garlic : ICellType
    {
        public Garlic()
        {
            Offset = 24;
            Mask = 0b1111 << Offset;
            Weight = 0b0001 << Offset;
        }

        public int Offset { get; }
        public int Mask { get; }
        public int Weight { get; }
    }
}