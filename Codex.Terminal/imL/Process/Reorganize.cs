#if (NET35 == false)

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
                string __GROUPBY = "yyyy-MM-dd";

                if (_args != null && _args.Length > 0)
                {
                    _HELP = _args.Count(_c => _c.ToUpper() == "-HELP" || _c.ToUpper() == "-H") > 0;

                    _SUBDIRECTORY = _args.Count(_c => _c.ToUpper() == "-SUBDIRECTORY" || _c.ToUpper() == "-S") > 0;
                    _OMIT_NOW = _args.Count(_c => _c.ToUpper() == "-OMIT-NOW" || _c.ToUpper() == "-O-N") > 0;
                    _OMIT_HIDDEN = _args.Count(_c => _c.ToUpper() == "-OMIT-HIDDEN" || _c.ToUpper() == "-O-H") > 0;

                    __FROM = _args.SkipWhile(_sw => _sw.ToUpper() != "--FROM").Skip(1).FirstOrDefault();
                    __TO = _args.SkipWhile(_sw => _sw.ToUpper() != "--TO").Skip(1).FirstOrDefault();
                    __GROUPBY = _args.SkipWhile(_sw => _sw.ToUpper() != "--GROUPBY").Skip(1).FirstOrDefault();
                }

                if (__TO == null)
                    __TO = __FROM;
                switch (__GROUPBY)
                {
                    case "yyyy":
                    case "yyyy-MM":
                    case "yyyy-MM-dd":
                        break;
                    default:
                        __GROUPBY = "yyyy-MM-dd";
                        break;
                }

                List<FileInfo> _list = new List<FileInfo>();

                if (Directory.Exists(__FROM))
#if (NETSTANDARD1_3 == false)
                    using (ElapsedTime _pb = new ElapsedTime())
                        GetFiles(ref _list, __FROM, _SUBDIRECTORY);
#else
                    GetFiles(ref _list, __FROM, _SUBDIRECTORY);
#endif
                else
                    throw new Exception("Directory.Exists == false");

                Console.WriteLine("Count: {0}", _list.Count);

                if (_list.Count == 0)
                    return;

                if (_OMIT_NOW)
                    _list.RemoveAll(_r => _r.CreationTime.ToDate() == _NOWDATE);
                if (_OMIT_HIDDEN)
                    _list.RemoveAll(_r => _r.Attributes.HasFlag(FileAttributes.Hidden));

                Console.WriteLine("Count after omit: {0}", _list.Count);

                if (_list.Count == 0)
                    return;

                _list = _list.OrderBy(_o => _o.CreationTime).ToList();

                DateTime[] _datetimes = _list.Select(_s => _s.CreationTime.ToDate())
                    .Distinct()
                    .ToArray();

                string[] _paths;
                switch (__GROUPBY)
                {
                    case "yyyy":
                        _paths = _datetimes.Select(_s => Path.Combine(_s.Year.ToString("0000")))
                            .Distinct()
                            .ToArray();
                        break;
                    case "yyyy-MM":
                        _paths = _datetimes.Select(_s => Path.Combine(_s.Year.ToString("0000"),
                            _s.Month.ToString("00")
                            ))
                            .Distinct()
                            .ToArray();
                        break;
                    default:
                        _paths = _datetimes.Select(_s => Path.Combine(_s.Year.ToString("0000"),
                            _s.Month.ToString("00"),
                            _s.Day.ToString("00")
                            ))
                            .Distinct()
                            .ToArray();
                        break;
                }

                using (ProgressBar64 _pb = new ProgressBar64(_paths.Length))
                {
                    foreach (string _item in _paths)
                    {
                        Directory.CreateDirectory(Path.Combine(__TO, _item));

                        _pb.Report();
                    }
                }

                using (ProgressBar64 _pb = new ProgressBar64(_list.Count))
                {
                    foreach (FileInfo _item in _list)
                    {
                        string _dest;

                        switch (__GROUPBY)
                        {
                            case "yyyy":
                                _dest = Path.Combine(__TO,
                                    _item.CreationTime.Year.ToString("0000"),
                                    _item.Name
                                    );
                                break;
                            case "yyyy-MM":
                                _dest = Path.Combine(__TO,
                                    _item.CreationTime.Year.ToString("0000"),
                                    _item.CreationTime.Month.ToString("00"),
                                    _item.Name
                                    );
                                break;
                            default:
                                _dest = Path.Combine(__TO,
                                    _item.CreationTime.Year.ToString("0000"),
                                    _item.CreationTime.Month.ToString("00"),
                                    _item.CreationTime.Day.ToString("00"),
                                    _item.Name
                                    );
                                break;
                        }

                        _item.MoveTo(_dest);

                        _pb.Report();
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }
        }
        public static void GetFiles(ref List<FileInfo> _list, string _directory, bool _sub = false)
        {
            string[] _files = Directory.GetFiles(_directory);
            foreach (string _item in _files)
                _list.Add(new FileInfo(_item));

            if (_sub)
            {
                string[] _subdirectory = Directory.GetDirectories(_directory);
                foreach (string _item in _subdirectory)
                    GetFiles(ref _list, _item, _sub);
            }
        }
    }
}
#endif