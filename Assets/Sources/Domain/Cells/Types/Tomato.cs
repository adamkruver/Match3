namespace Match3.Domain.Types
{
    public class Tomato : ICellType
    {
        public Tomato()
        {
            Offset = 20;
            Mask = 0b1111L << Offset;
            Weight = 0b0001L << Offset;
        }

        public int Offset { get; }
        public long Mask { get; }
        public long Weight { get; }
    }
}