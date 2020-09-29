#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Drawing;
using System.Text;

namespace Codex.Helper
{
    public static class ConsoleHelper
    {
        public static void InitEncoding()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void Write(Point _xy, Char _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.Write(_value);
        }
        public static void Write(Point _xy, String _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.Write(_value);
        }
        public static void WriteLine(Point _xy, Char _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.WriteLine(_value);
        }
        public static void WriteLine(Point _xy, String _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.WriteLine(_value);
        }
    }
}

#endif