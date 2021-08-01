
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Codex.Utility.Terminal.Process
{
    public static class Reorganize
    {
        public static void Main(string[] _args, bool _throw = false)
        {
            try
            {
                DateTime _NOW = DateTime.Now;
                DateTime _NOWDATE = _NOW.Date;

                bool _HELP = false;

                bool _SUBDIRECTORY = false;
                bool _OMIT_NOW = false;
                bool _OMIT_HIDDEN = false;

                string __FROM = @"e:\Tmp2";
                string __TO = @"e:\TestTmp2";
                string __GROUPBY = "yyyy-MM-dd";

                if (_args != null && _args.Length > 0)
                {
                    _HELP = _args.ArgAppear("-HELP", "-H");

                    _SUBDIRECTORY = _args.ArgAppear("-SUBDIRECTORY", "-S");
                    _OMIT_NOW = _args.ArgAppear("-OMIT-NOW", "-O-N");
                    _OMIT_HIDDEN = _args.ArgAppear("-OMIT-HIDDEN", "-O-H");

                    __FROM = _args.ArgValue("--FROM");
                    __TO = _args.ArgValue("--TO");
                    __GROUPBY = _args.ArgValue("--GROUPBY");
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
                    throw new ArgumentException("Directory.Exists == false");

                Console.WriteLine("Count: {0}", _list.Count);

                if (_list.Count == 0)
                    return;

                if (_OMIT_NOW)
                    _list.RemoveAll(_r => _r.CreationTime.Date == _NOWDATE);
                if (_OMIT_HIDDEN)
                    _list.RemoveAll(_r => _r.Attributes.HasFlag(FileAttributes.Hidden));

                Console.WriteLine("Count after omit: {0}", _list.Count);

                if (_list.Count == 0)
                    return;

                _list = _list.OrderBy(_o => _o.CreationTime).ToList();

                DateTime[] _dates = _list.Select(_s => _s.CreationTime.Date)
                    .Distinct()
                    .ToArray();

                string[] _paths;
                switch (__GROUPBY)
                {
                    case "yyyy":
                        _paths = _dates.Select(_s => PathHelper.Combine(_s.Year.ToString("0000")))
                            .Distinct()
                            .ToArray();
                        break;
                    case "yyyy-MM":
                        _paths = _dates.Select(_s => PathHelper.Combine(_s.Year.ToString("0000"),
                            _s.Month.ToString("00")
                            ))
                            .Distinct()
                            .ToArray();
                        break;
                    default:
                        _paths = _dates.Select(_s => PathHelper.Combine(_s.Year.ToString("0000"),
                            _s.Month.ToString("00"),
                            _s.Day.ToString("00")
                            ))
                            .Distinct()
                            .ToArray();
                        break;
                }

                using (ProgressBar32 _pb = new ProgressBar32(_paths.Length))
                {
                    foreach (string _item in _paths)
                    {
                        Directory.CreateDirectory(PathHelper.Combine(__TO, _item));

                        _pb.Report();
                    }
                }

                using (ProgressBar32 _pb = new ProgressBar32(_list.Count))
                {
                    foreach (FileInfo _item in _list)
                    {
                        string _dest;

                        switch (__GROUPBY)
                        {
                            case "yyyy":
                                _dest = PathHelper.Combine(__TO,
                                    _item.CreationTime.Year.ToString("0000"),
                                    _item.Name
                                    );
                                break;
                            case "yyyy-MM":
                                _dest = PathHelper.Combine(__TO,
                                    _item.CreationTime.Year.ToString("0000"),
                                    _item.CreationTime.Month.ToString("00"),
                                    _item.Name
                                    );
                                break;
                            default:
                                _dest = PathHelper.Combine(__TO,
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
                if (_throw)
                    throw _ex;

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