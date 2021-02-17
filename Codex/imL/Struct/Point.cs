namespace Codex.Struct
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;
        }
    }
}