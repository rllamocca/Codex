#if (NETSTANDARD1_3)
using Codex.Struct;
#else
using System.Drawing;
#endif

using System;
using System.Text;

namespace Codex.Terminal.Helper
{
    public static class ConsoleHelper
    {
        public static void InitEncoding()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void Write(Point _xy, char _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.Write(_value);
        }
        public static void Write(Point _xy, string _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.Write(_value);
        }
        public static void WriteLine(Point _xy, char _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.WriteLine(_value);
        }
        public static void WriteLine(Point _xy, string _value)
        {
            Console.CursorLeft = _xy.X;
            Console.CursorTop = _xy.Y;
            Console.WriteLine(_value);
        }
    }
}