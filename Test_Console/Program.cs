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

            Console.ReadKey();
        }
    }
}
