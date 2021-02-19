#if(NET35 == false)

using Codex.Extension;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Codex.Terminal.Process
{
    public static class Reorganize
    {
        public static void Main(string[] _args)
        {
            try
            {
                DateTime _NOW = DateTime.Now;
                DateTime _NOWDATE = new DateTime(_NOW.Year, _NOW.Month, _NOW.Day);

                bool _HELP = false;
                bool _SUBDIRECTORY = false;
                bool _OMIT_NOW = false;
                bool _OMIT_HIDDEN = false;

                string __FROM = @"e:\Tmp2";
                string __TO = @"e:\TestTmp2";
                string __PATHFORMAT = "yyyy-MM-dd";

                if (_args != null && _args.Length > 0)
                {
                    _HELP = _args.Count(_c => _c.ToUpper() == "-HELP" || _c.ToUpper() == "-H") > 0;

                    _SUBDIRECTORY = _args.Count(_c => _c.ToUpper() == "-SUBDIRECTORY" || _c.ToUpper() == "-S") > 0;
                    _OMIT_NOW = _args.Count(_c => _c.ToUpper() == "-OMIT-NOW" || _c.ToUpper() == "-O-N") > 0;
                    _OMIT_HIDDEN = _args.Count(_c => _c.ToUpper() == "-OMIT-HIDDEN" || _c.ToUpper() == "-O-H") > 0;

                    __FROM = _args.SkipWhile(_sw => _sw.ToUpper() != "--FROM").Skip(1).FirstOrDefault();
                    __TO = _args.SkipWhile(_sw => _sw.ToUpper() != "--TO").Skip(1).FirstOrDefault();
                    __PATHFORMAT = _args.SkipWhile(_sw => _sw.ToUpper() != "__PATHFORMAT").Skip(1).FirstOrDefault();
                }

                if (__TO == null)
                    __TO = __FROM;
                switch (__PATHFORMAT)
                {
                    case "yyyy-MM-dd":
                    case "yyyy-MM":
                    case "yyyy":
                        break;
                    default:
                        __PATHFORMAT = "yyyy-MM-dd";
                        break;
                }
                    

                Console.WriteLine("__FROM: {0}", __FROM);
                Console.WriteLine("__TO: {0}", __TO);
                Console.WriteLine("__SUBDIRECTORY: {0}", _SUBDIRECTORY);

                List<FileInfo> _list = new List<FileInfo>();

                if (Directory.Exists(__FROM))
                    //using (ElapsedTime _pb = new ElapsedTime())
                    GetFiles(ref _list, __FROM, _SUBDIRECTORY);
                else
                    throw new Exception("Directory.Exists == false");

                Console.WriteLine("Count: {0}", _list.Count);

                if (_OMIT_NOW)
                    _list.RemoveAll(_r => _r.CreationTime.ToDate() == _NOWDATE);
                if (_OMIT_HIDDEN)
                    _list.RemoveAll(_r => _r.Attributes.HasFlag(FileAttributes.Hidden));

                Console.WriteLine("Count after: {0}", _list.Count);

                DateTime[] _datetimes = _list
                    .Select(_s => _s.CreationTime.ToDate())
                    .Distinct()
                    .ToArray();

                using (ProgressBar64 _pb = new ProgressBar64(_datetimes.Length))
                {
                    foreach (DateTime _item in _datetimes)
                    {
                        string _path = Path.Combine(__TO,
                            _item.Year.ToString("0000"),
                            _item.Month.ToString("00"),
                            _item.Day.ToString("00")
                            );
                        Directory.CreateDirectory(_path);

                        _pb.Report();
                    }
                }

                using (ProgressBar64 _pb = new ProgressBar64(_list.Count))
                {
                    foreach (FileInfo _item in _list)
                    {
                        string _path = Path.Combine(__TO,
                                _item.CreationTime.Year.ToString("0000"),
                                _item.CreationTime.Month.ToString("00"),
                                _item.CreationTime.Day.ToString("00"),
                                _item.Name
                            );

                        _item.MoveTo(_path);

                        _pb.Report();
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }
        }
        public static void GetFiles(ref List<FileInfo> _list, string _directory, bool _subs = false)
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
                    GetFiles(ref _list, _item, _subs);
            }
        }
    }
}
#endif