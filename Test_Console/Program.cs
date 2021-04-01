using Codex.Terminal;
using Codex.Terminal.Helper;

using System;
using System.Collections;
using System.Text;
using System.Threading;

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

            int _max = 20;
            long _max2 = 30;

            ElapsedTime _et = new();
            ProgressBar32 _pb = new(_max, _et);
            ProgressBar64 _pb2 = new(_max2, _pb);
            for (int _i = 1; _i <= _pb.Length; _i++)
            {
                Thread.Sleep(100);
                _pb.Report(_i);
            }
            for (long _j = 1; _j <= _pb2.Length; _j++)
            {
                Thread.Sleep(100);
                _pb2.Report(_j);
            }
            _pb2.Dispose();
            _pb.Dispose();
            _et.Dispose();

            /*
            // Creates and initializes a new Queue.
            Queue myQ = new Queue();
            myQ.Enqueue("The");
            myQ.Enqueue("quick");
            myQ.Enqueue("brown");
            myQ.Enqueue("fox");

            // Displays the Queue.
            Console.Write("Queue values:");
            PrintValues(myQ);

            // Removes an element from the Queue.
            Console.WriteLine("(Dequeue)\t{0}", myQ.Dequeue());

            // Displays the Queue.
            Console.Write("Queue values:");
            PrintValues(myQ);

            // Removes another element from the Queue.
            Console.WriteLine("(Dequeue)\t{0}", myQ.Dequeue());

            // Displays the Queue.
            Console.Write("Queue values:");
            PrintValues(myQ);

            // Views the first element in the Queue but does not remove it.
            Console.WriteLine("(Peek)   \t{0}", myQ.Peek());

            // Displays the Queue.
            Console.Write("Queue values:");
            PrintValues(myQ);
            */

            //################################################################

#if DEBUG
            ConsoleHelper.End(true, false);
#else
            ConsoleHelper.End()
#endif
        }

        public static void PrintValues(IEnumerable myCollection)
        {
            foreach (object obj in myCollection)
                Console.Write("    {0}", obj);
            Console.WriteLine();
        }

        public static void EncodingInfo()
        {
            // Print the header.
            Console.Write("Info.CodePage      ");
            Console.Write("Info.Name                    ");
            Console.Write("Info.DisplayName");
            Console.WriteLine();

            // Display the EncodingInfo names for every encoding, and compare with the equivalent Encoding names.
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();

                Console.Write("{0,-15}", ei.CodePage);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.Write("{0,-25}", ei.Name);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.Write("{0,-25}", ei.DisplayName);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.WriteLine();
            }
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
