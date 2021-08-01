using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Codex.Utility.Ftp
{
    public static class FtpHelper
    {
        public static void Init_FtpWebRequest(ref FtpWebRequest _ref, FormatFtp _format)
        {
            _ref.UseBinary = _format.UseBinary ?? _ref.UseBinary;
            _ref.Timeout = _format.Timeout ?? _ref.Timeout;
            _ref.ReadWriteTimeout = _format.ReadWriteTimeout ?? _ref.ReadWriteTimeout;
            _ref.KeepAlive = _format.KeepAlive ?? _ref.KeepAlive;
            _ref.EnableSsl = _format.EnableSsl ?? _ref.EnableSsl;
            _ref.UsePassive = _format.UsePassive ?? _ref.UsePassive;

            _ref.Credentials = new NetworkCredential(_format.UserName, _format.Password);
        }

        private static FtpWebRequest Create(FormatFtp _format)
        {
            FtpWebRequest _fwr = (FtpWebRequest)FtpWebRequest.Create(_format.Host + _format.Path);
            FtpHelper.Init_FtpWebRequest(ref _fwr, _format);

            return _fwr;
        }

        public static FtpStatusCode From_ListDirectory(out string[] _list, FormatFtp _format)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_format);
            _ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            List<string> _return = new List<string>();
            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_response.GetResponseStream()))
                    while (_sr.EndOfStream == false)
                        _return.Add(_sr.ReadLine());

                _list = _return.ToArray();
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode From_DownloadFile(ref Stream _rec, FormatFtp _format)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_format);
            _ftp.Method = WebRequestMethods.Ftp.DownloadFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_response.GetResponseStream()))
                {
                    StreamWriter _sw = new StreamWriter(_rec);
                    _sw.Write(_sr.ReadToEnd());
                    _sw.Flush();
                }
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_UploadFile(Stream _sub, FormatFtp _format)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_format);
            _ftp.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream _s = _ftp.GetRequestStream())
            {
#if (NET35)
                _sub.OldCopyTo(_s);
#else
                _sub.CopyTo(_s);
#endif

            }

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_DeleteFile(FormatFtp _format)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_format);
            _ftp.Method = WebRequestMethods.Ftp.DeleteFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }
    }
}