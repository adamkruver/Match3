﻿namespace Match3.Damain.Sources.Domain.Tables
{
    public class Table
    {
        public Table(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int Height { get; }
        public int Width { get; }
    }
}