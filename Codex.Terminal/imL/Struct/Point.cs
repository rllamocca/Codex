#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

namespace Codex.Terminal.Struct
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
#endif