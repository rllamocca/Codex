using Codex.Terminal.Helper;
using Codex.Terminal.Process;

using System;
using System.Linq;

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

        static void Main(string[] _args)
        {
            ConsoleHelper.InitEncoding();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            //################################################################


            string[] _list = new string[] { "a", "b", "c", "d", "e", "f" };

            string _skip = _list.SkipWhile(_sw => _sw.ToUpper() != "F").Skip(1).FirstOrDefault();

            _skip = _skip == null ? "NULL" : _skip;

            Console.WriteLine("_skip : {0}", _skip);

            //Reorganize.Main(_args);

            //################################################################

            ConsoleHelper.Ends();
        }
    }

    //public class CachedTimeSource
    //{
    //    private int _LTC = -1;
    //    private DateTime _LDT = DateTime.MinValue;

    //    public DateTime FreshTime { get { return DateTime.Now; } }
    //    public DateTime Last
    //    {
    //        get
    //        {
    //            int _tc = Environment.TickCount;
    //            if (_tc == this._LTC)
    //                return this._LDT;
    //            else
    //            {
    //                DateTime _return = this.FreshTime;
    //                this._LTC = _tc;
    //                this._LDT = _return;
    //                return _return;
    //            }
    //        }
    //    }
    //}
}
