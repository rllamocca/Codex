#if (NETSTANDARD1_3)
using Codex.Struct;
#else
using System.Drawing;
#endif

using Codex.Helper;

using System;
using System.Text;

namespace Codex.Utility.Terminal.Helper
{
    public static class ConsoleHelper
    {
        public static void Init(bool _enc = true, string _title = null)
        {
            Console.WriteLine();
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

        public static void End(bool _rk = false, bool _card = true)
        {
            Console.WriteLine();
            if (_card)
                Console.WriteLine(@" ♫♪ ... {0}", StringHelper.MyFortuneCard());
            else
                Console.WriteLine(@" ♫♪ ... {0}", StringHelper.MyFortune());

            if (_rk)
            {
                Console.WriteLine(@" (Press any key to exit) ");
                Console.ReadKey();
            }
            Console.WriteLine();
        }

        public static void Write(Point _xy, char _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.Write(_value);
        }
        public static void Write(Point _xy, string _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.Write(_value);
        }
        public static void WriteLine(Point _xy, char _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.WriteLine(_value);
        }
        public static void WriteLine(Point _xy, string _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.WriteLine(_value);
        }
    }
}