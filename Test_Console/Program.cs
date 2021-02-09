using Codex.Generic;
using Codex.ORM;
using Codex.ORM.Pattern;
using Codex.ORM.Sql;
using Codex.Struct;


using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (ProgressBar64 _pb = new ProgressBar64(0))
            {
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(500);
                }
            }

            Console.ReadKey();
        }
    }
}
