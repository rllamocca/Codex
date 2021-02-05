using Codex.ORM;
using Codex.ORM.Pattern;
using Codex.ORM.Sql;
using Codex.Struct;


using System;
using System.Data;
using System.Linq;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SColumn[] _cols = new SColumn[3];
            _cols[0] = new SColumn("pk", true, true);
            _cols[1] = new SColumn("col1", true);
            _cols[2] = new SColumn("col2", true);

            STable _table = new STable("_table", _cols);

            string _sql = _sql_insert(_table, true);
            Console.WriteLine(_sql);

            IOrmParameter[] _a = new IOrmParameter[3];
            _a[0] = new OrmSqlParameter("pk", "value1", SqlDbType.NVarChar, 50);
            _a[1] = new OrmSqlParameter("col1", "GETDATE()");
            _a[2] = new OrmSqlParameter("col2", 1, SqlDbType.TinyInt);

            _sql = SqlPattern.Insert("_table", _a, false);
            Console.WriteLine(_sql);

            Console.ReadKey();
        }

        public static string _sql_insert(STable _a, bool _scope_identity = false)
        {
            string _pattern = @"
 INSERT INTO {0} ({1}) VALUES ({2});
 {3}
";
            string _0 = null;
            string _1 = null;
            string _2 = null;
            string _3 = null;


            _0 = string.Format("[{0}]", _a.Name);
            string[] _tmp = _a.Columns.Where(_w => (_w.IsPrimaryKey && _w.IsIdentity) == false).Select(_s => string.Format("[{0}]", _s.Name)).ToArray();
            _1 = string.Join(",", _tmp);
            _tmp = _a.Columns.Where(_w => (_w.IsPrimaryKey && _w.IsIdentity) == false).Select(_s => string.Format("@_{0}_", _s.Name)).ToArray();
            _2 = string.Join(",", _tmp);

            if (_scope_identity)
                _3 = "SELECT SCOPE_IDENTITY();";

            return string.Format(_pattern, _0, _1, _2, _3);
        }
    }
}
