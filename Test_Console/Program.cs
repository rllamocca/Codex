using Codex;


using System;
using System.Threading;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            long _max0 = 5;
            long _max1 = 10;
            long _max2 = 15;
            using (ProgressBar64 _pb0 = new ProgressBar64(_max0))
            {
                for (long _n0 = 1; _n0 <= _max0; _n0++)
                {
                    using (ProgressBar64 _pb1 = new ProgressBar64(_max1, _pb0))
                    {
                        for (long _n1 = 1; _n1 <= _max1; _n1++)
                        {

                            using (ProgressBar64 _pb2 = new ProgressBar64(_max2, _pb1))
                            {
                                for (long _n2 = 1; _n2 <= _max2; _n2++)
                                {
                                    Thread.Sleep(10);
                                    _pb2.Report(_n2);
                                }
                            }
                            _pb1.Report(_n1);
                        }
                    }
                    _pb0.Report(_n0);
                }
            }

            using (ProgressBar64 _pb = new ProgressBar64(0))
            {
                for (int _n0 = 1; _n0 <= _max2; _n0++)
                    Thread.Sleep(500);
            }


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
            

            Console.ReadKey();
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
