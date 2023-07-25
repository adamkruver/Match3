namespace Match3.Domain.Types
{
    public class Multi : ICellType
    {
        public Multi()
        {
            Offset = 28;
            Mask = 0b1111L << Offset;
            Weight = 0b0001L << Offset;
        }

        public int Offset { get; }
        public long Mask { get; }
        public long Weight { get; }
    }
}