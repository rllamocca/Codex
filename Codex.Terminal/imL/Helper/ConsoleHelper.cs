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
        public static void Init(bool _enc = true, string _title = null)
        {
            if (_enc)
            {
                Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Encoding.UTF8;
            }
            if (_title != null)
                Console.Title = _title;

            Console.WriteLine(@" Start the magic trick ... ♪♫ ");
            Console.WriteLine();
        }

        public static void End(bool _rk = false)
        {
            Console.WriteLine();
            Console.WriteLine(@" ♫♪ ... ♥♦♣♠ ...");

            if (_rk)
            {
                Console.WriteLine(@" (Press any key to exit) ");
                Console.ReadKey();
            }
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