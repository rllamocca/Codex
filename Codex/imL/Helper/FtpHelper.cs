#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Config;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Codex.Helper
{
    public static class FtpHelper
    {
        private static FtpWebRequest NewFTP(FtpConfig _config)
        {
            FtpWebRequest _create = (FtpWebRequest)FtpWebRequest.Create(_config.Uri + "/" + _config.FileName);
            _create.UseBinary = _config.UseBinary;
            _create.KeepAlive = _config.KeepAlive;
            _create.UsePassive = _config.UsePassive;
            _create.EnableSsl = _config.EnableSsl;
            _create.Credentials = new NetworkCredential(_config.UserName, _config.Password);

            return _create;
        }

        public static FtpStatusCode From_Ftp(ref String[] _from, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.NewFTP(_config);
            _ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            List<String> _return = new List<String>();
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

        public static FtpStatusCode From_Ftp(ref Stream _from, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.NewFTP(_config);
            _ftp.Method = WebRequestMethods.Ftp.DownloadFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_response.GetResponseStream()))
                {
                    using (StreamWriter _sw = new StreamWriter(_from))
                    {
                        _sw.Write(_sr.ReadToEnd());
                        _sw.Flush();
                    }
                }
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_Ftp226(Stream _to, FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.NewFTP(_config);
            _ftp.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream _s = _ftp.GetRequestStream())
            {
                _to.CopyTo(_s);
            }

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }

        public static FtpStatusCode To_Ftp(FtpConfig _config)
        {
            FtpWebRequest _ftp = FtpHelper.NewFTP(_config);
            _ftp.Method = WebRequestMethods.Ftp.DeleteFile;

            using (FtpWebResponse _response = (FtpWebResponse)_ftp.GetResponse())
            {
                return _response.StatusCode;
            }
        }
    }
}

#endif