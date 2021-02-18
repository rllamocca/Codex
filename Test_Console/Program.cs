﻿using Codex.Terminal;
using Codex.Terminal.Helper;

using System;
using System.Collections.Generic;
using System.Threading;

using System.Linq;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.InitEncoding();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            Console.WriteLine("Hello World!");
            //################################################################
            try
            {
                long _max0 = 5;
                long _max1 = 5;
                long _max2 = 5;

                using (ElapsedTime _pb = new ElapsedTime())
                {
                    for (int _n0 = 1; _n0 <= _max2; _n0++)
                        Thread.Sleep(500);
                }

                using (ProgressBar64 _pb = new ProgressBar64(0))
                {
                    for (int _n0 = 1; _n0 <= _max2; _n0++)
                        Thread.Sleep(500);
                }

                //using (ProgressBar64 _pb0 = new ProgressBar64(_max0))
                //{
                //    for (long _n0 = 1; _n0 <= _max0; _n0++)
                //    {
                //        using (ProgressBar64 _pb1 = new ProgressBar64(_max1, _pb0))
                //        {
                //            for (long _n1 = 1; _n1 <= _max1; _n1++)
                //            {

                //                using (ProgressBar64 _pb2 = new ProgressBar64(_max2, _pb1))
                //                {
                //                    for (long _n2 = 1; _n2 <= _max2; _n2++)
                //                    {
                //                        Thread.Sleep(20);
                //                        _pb2.Report(_n2);
                //                    }
                //                }
                //                _pb1.Report(_n1);
                //            }
                //        }
                //        _pb0.Report(_n0);
                //    }
                //}

                //using (ElapsedTime _pb = new ElapsedTime())
                //{
                //    for (int _n0 = 1; _n0 <= _max2; _n0++)
                //        Thread.Sleep(500);
                //}


                //CachedTimeSource _time = new CachedTimeSource();
                //for (int _n0 = 1; _n0 <= _max2; _n0++)
                //{
                //    Thread.Sleep(10);
                //    Console.WriteLine(_time.Last.ToString("HH:mm:ss.fff"));
                //}
                //Console.WriteLine("...");
                //for (int _n0 = 1; _n0 <= _max2; _n0++)
                //{
                //    Thread.Sleep(10);
                //    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                //}
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }
            Console.ReadKey();
        }

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
    }

    public class CachedTimeSource
    {
        private int _LTC = -1;
        private DateTime _LDT = DateTime.MinValue;

        public DateTime FreshTime { get { return DateTime.Now; } }
        public DateTime Last
        {
            get
            {
                int _tc = Environment.TickCount;
                if (_tc == this._LTC)
                    return this._LDT;
                else
                {
                    DateTime _return = this.FreshTime;
                    this._LTC = _tc;
                    this._LDT = _return;
                    return _return;
                }
            }
        }
    }
}
