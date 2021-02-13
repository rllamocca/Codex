#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1)
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using Codex.Struct;
#else
using System.Drawing;
#endif

using System;
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