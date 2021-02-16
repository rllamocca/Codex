#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Config;
using Codex.Extension;

using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Codex.Helper
{
    public static class FtpHelper
    {
        private static FtpWebRequest Create(FtpConfig _config)
        {
            FtpWebRequest _create = (FtpWebRequest)FtpWebRequest.Create(_config.Uri + "/" + _config.FileName);
            _create.UseBinary = _config.UseBinary;
            _create.KeepAlive = _config.KeepAlive;
            _create.UsePassive = _config.UsePassive;
            _create.EnableSsl = _config.EnableSsl;
            _create.Credentials = new NetworkCredential(_config.UserName, _config.Password);

            return _create;
        }

        public static FtpStatusCode From_ListDirectory(ref string[] _from, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_config);
            _ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            List<string> _return = new List<string>();
            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_response.GetResponseStream()))
                {
                    while (_sr.EndOfStream == false)
                        _return.Add(_sr.ReadLine());

                    _from = _return.ToArray();
                }
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode From_DownloadFile(ref Stream _rec, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_config);
            _ftp.Method = WebRequestMethods.Ftp.DownloadFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_response.GetResponseStream()))
                {
                    using (StreamWriter _sw = new StreamWriter(_rec))
                    {
                        _sw.Write(_sr.ReadToEnd());
                        _sw.Flush();
                    }
                }
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_UploadFile(Stream _sub, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_config);
            _ftp.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream _s = _ftp.GetRequestStream())
            {
#if (NET35)
                _sub.OldCopy(_s);
#else
                _sub.CopyTo(_s);
#endif

            }

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_DeleteFile(FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.Create(_config);
            _ftp.Method = WebRequestMethods.Ftp.DeleteFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }
    }
}
#endif