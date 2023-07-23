namespace Match3.Domain
{
    public interface ICellType
    {
        int Offset { get; }
        int Mask { get; }
        int Weight { get; }
    }
}