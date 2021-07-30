#if (NETSTANDARD1_3)
using System;

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

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}
#endif