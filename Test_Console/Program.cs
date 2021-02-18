using Codex.Extension;
using Codex.Terminal.Helper;
using Codex.Terminal;

using System;
using System.Collections.Generic;
using System.IO;
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

            Console.WriteLine("Hello World!");

            //################################################################
            try
            {
                string _from = @"e:\Tmp2";
                string _to = @"e:\TestTmp2";
                bool _subdirectory = false;

                if (_args != null && _args.Length > 0)
                {
                    switch (_args.Length)
                    {
                        case 1:
                            _from = _args[0];
                            _to = _from;
                            break;
                        case 2:
                            _from = _args[0];
                            _to = _args[1];
                            break;
                        case 3:
                            _from = _args[0];
                            _to = _args[1];
                            _subdirectory = Convert.ToBoolean(_args[2]);
                            break;
                        default:
                            _from = null;
                            _to = null;
                            _subdirectory = false;
                            break;
                    }
                }

                Console.WriteLine("_from:{0} _to:{1} _subdirectory:{2}", _from, _to, _subdirectory);

                DateTime _now = DateTime.Now;
                DateTime _nowdate = new DateTime(_now.Year, _now.Month, _now.Day);

                List<FileInfo> _list = new List<FileInfo>();

                if (Directory.Exists(_from))
                    using (ElapsedTime _pb = new ElapsedTime())
                        ProcessDirectory(ref _list, _from, _subdirectory);
                else
                    throw new Exception("Directory.Exists == false");
                Console.WriteLine("Count {0}", _list.Count);

                DateTime[] _datetimes = _list
                    .Select(_s => _s.CreationTime.ToDate())
                    .Distinct()
                    .ToArray();

                using (ProgressBar64 _pb = new ProgressBar64(_datetimes.Length))
                {
                    foreach (DateTime _item in _datetimes)
                    {
                        Directory.CreateDirectory(
                            Path.Combine(_to,
                            _item.Year.ToString("0000"),
                            _item.Month.ToString("00"),
                            _item.Day.ToString("00")
                            ));

                        _pb.Report();
                    }
                }

                using (ProgressBar64 _pb = new ProgressBar64(_list.Count))
                {
                    foreach (FileInfo _item in _list)
                    {
                        if (_item.CreationTime.ToDate() != _nowdate)
                        {
                            string _move = Path.Combine(_to,
                                _item.CreationTime.Year.ToString("0000"),
                                _item.CreationTime.Month.ToString("00"),
                                _item.CreationTime.Day.ToString("00"),
                                _item.Name
                            );

                            _item.MoveTo(_move);
                        }

                        _pb.Report();
                    }
                }

            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            //################################################################

            Console.WriteLine("Good bye World!");
            Console.ReadKey();
        }

        public static void ProcessDirectory(ref List<FileInfo> _list, string _directory, bool _subs = false)
        {
            string[] _files = Directory.GetFiles(_directory);
            foreach (string _item in _files)
            {
                FileInfo _fi = new FileInfo(_item);
                _list.Add(_fi);
            }

            if (_subs)
            {
                string[] _subdirectory = Directory.GetDirectories(_directory);
                foreach (string _item in _subdirectory)
                    ProcessDirectory(ref _list, _item, _subs);
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
