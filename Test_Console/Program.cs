using Codex.ORM.Enum;
using Codex.ORM.Helper;
using Codex.ORM.Sql.Helper;

using System;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //IOrmConnection _conn = new OrmSqlConnection("", false);
             IOrmAsyncHelper _helper = new SqlAsyncHelper();

            _helper.Execute(null, null, EExecute.NonQuery, null);
            _helper.Get_DataTable(null, null, null);
        }
    }
}
