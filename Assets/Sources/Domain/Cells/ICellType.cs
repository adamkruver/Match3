using System;

namespace Match3.Domain
{
    public interface ICellType
    {
        int Offset { get; }
        long Mask { get; }
        long Weight { get; }
    }
}