using Codex.Terminal.Helper;

using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.Util;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_Console
{
    class Program
    {
        /*
CTRL_C_EVENT
CTRL_BREAK_EVENT
*/
        static void CancelKeyPress(object _sender, ConsoleCancelEventArgs _args)
        {
            Console.WriteLine("CancelKeyPress ...");
            Console.WriteLine($" SpecialKey pressed: {_args.SpecialKey}\n");

            /*
CTRL_LOGOFF_EVENT
CTRL_SHUTDOWN_EVENT
             */

            //Console.WriteLine($"  Cancel property: {_args.Cancel}");
            // Set the Cancel property to true to prevent the process from terminating.
            //_args.Cancel = true;
        }

        /*
CTRL_CLOSE_EVENT
        */
        static void ProcessExit(object _sender, EventArgs _args)
        {
            Console.WriteLine("ProcessExit ...");
        }

        async static Task Main(
            //string[] _args
            )
        {
            ConsoleHelper.Init();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            //################################################################


            //################################################################

#if DEBUG
            ConsoleHelper.End(true, false);
#else
            ConsoleHelper.End();
#endif
        }

        static string ToStringBits(byte[] _array)
        {
            if (_array == null)
                return null;

            string _return = null;
            for (uint _i = 0; _i < _array.Length; _i++)
                _return += string.Format("[{0}] -> {1}\n", _i, Convert.ToString(_array[_i], 2).PadLeft(8, '0'));

            return _return;
        }
        static string ToStringBytes(byte[] _array)
        {
            if (_array == null)
                return null;

            string _return = null;
            for (uint _i = 0; _i < _array.Length; _i++)
                _return += string.Format("[{0}] -> {1}\n", _i, _array[_i]);

            return _return;
        }
    }
}
