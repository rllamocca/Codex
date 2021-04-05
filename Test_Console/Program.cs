using Codex.Terminal;
using Codex.Terminal.Helper;
using Codex.NPOI.Helper;

using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Data;

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

        static void Main(
            //string[] _args
            )
        {
            ConsoleHelper.Init();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            //################################################################

            DataSet _ds = NPOIHelper.To_DataSet(@"F:\Tmp\_tmp.xlsx", _xls: false);
            DataTable _dt = NPOIHelper.To_DataTable(@"F:\Tmp\_tmp.xlsx", _xls: false);
            VO_RawFaraggi[] _tmp = NPOIHelper.To_Generic<VO_RawFaraggi>(@"F:\Tmp\_tmp.xlsx", _xls: false);

            if (true)
            {

            }

            //################################################################

#if DEBUG
            ConsoleHelper.End(true, false);
#else
            ConsoleHelper.End()
#endif
        }


    }
}
