#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

using Codex.Enum;
using Codex.Helper;

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codex
{
    public class Password : IDisposable
    {
        private const string _MI = "abcdefghijklmnñopqrstuvwxyz";
        private const string _MA = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        private const string _NU = "1234567890";
        private const string _ES = " |°¬#$%&=',;.:¨*+~-_^`´()[]{}<>¡!¿?/\\\"";
        //؟

        private char[] _BASE;
        private string _CONT = string.Empty;

        public bool Minusculas { set; get; } = true;
        public bool Mayusculas { set; get; } = true;
        public bool Numeros { set; get; } = true;
        public bool Especiales { set; get; } = true;
        public string Otros { set; get; } = "";

        public char[] Base { get { return this._BASE; } }
        public ERandomSort ReOrdenamiento { set; get; } = ERandomSort.Fisher_Yates;
        public string Contrasena { get { return this._CONT; } }

        public Return Generar(byte _largo = 8)
        {
            try
            {
                this._BASE = Password.PrepararBase(this.Minusculas, this.Mayusculas, this.Numeros, this.Especiales, this.Otros);
                this._BASE = Password.ReOrdenar(this._BASE, this.ReOrdenamiento);
                this._CONT = Password.Generar(this._BASE, _largo);
                return new Return(true, this._CONT);
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        public static char[] PrepararBase(bool _mi, bool _ma, bool _nu, bool _es, string _otros = "")
        {
            string _return = string.Empty;
            if (_mi) _return += Password._MI;
            if (_ma) _return += Password._MA;
            if (_nu) _return += Password._NU;
            if (_es) _return += Password._ES;
            if (_otros.Length > 0) _return += _otros;
            _return = _return.CleanTBLFCR();
            return _return.ToArray().Distinct().ToArray();
        }
        public static char[] ReOrdenar(char[] _data, ERandomSort _s = ERandomSort.Fisher_Yates)
        {
            switch (_s)
            {
                case ERandomSort.Fisher_Yates:
                    _T_Helper.Fisher_Yates(_data);
                    break;
                default:
                    break;
            }
            return _data;
        }
        public static string Generar(char[] _base, byte _largo = 8)
        {
            char[] _tmp = new char[_largo];
            Random _rdm = new Random();
            for (byte _i = 0; _i < _largo; _i++)
                _tmp[_i] = _base[_rdm.Next(0, _base.Length)];

            string[] _return = new string[_largo];
            for (byte _i = 0; _i < _largo; _i++)
                _return[_i] = Convert.ToString(_tmp[_i]);

            return string.Join("", _return);
        }
        private static double aparicion(byte napa, byte nmax)
        {
            if (napa > nmax)
                napa = nmax;
            return 20.00 * napa / nmax;
        }
        /// <summary>
        /// Valora una contraseña de : 0 - 6.
        /// </summary>
        /// <param name="password">Contraseña.</param>
        /// <returns></returns>
        public static byte valorar(string password)
        {
            double score = 0.0;
            if (password.Length != 0)
            {
                Match m;
                byte con = 0;
                byte por = (byte)(password.Length * 0.25 + 0.5);
                if (por == 0)
                    por = 1;

                if (password.Length <= 4)
                    score += 0;
                else if (password.Length <= 8)
                    score += 6;
                else if (password.Length <= 12)
                    score += 14;
                else if (password.Length <= 16)
                    score += 18;
                else
                    score += 20;
                con = 0;
                foreach (char item in _MI)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (char item in _MA)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (char item in _NU)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (char item in _ES)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                m = Regex.Match(password, "([a-z].*[A-Z])|([A-Z].*[a-z])");
                if (m.Success)
                    score += 5;
                m = Regex.Match(password, "([a-zA-Z0-9])|([0-9A-Za-z])");
                if (m.Success)
                    score += 5;
                m = Regex.Match(password, "([a-zA-Z0-9].*[|,°,¬,#,$,%,&,=,',,,;,.,:,¨,*,+,~,-,_,^,`,´,(,),[,],{,},<,>,¡,!,¿,?,/,\\,\",])|([|,°,¬,#,$,%,&,=,',,,;,.,:,¨,*,+,~,-,_,^,`,´,(,),[,],{,},<,>,¡,!,¿,?,/,\\,\",].*[a-zA-Z0-9])");
                if (m.Success)
                    score += 10;
            }
            score /= 20.00;
            if (score > 6)
                score = 6;
            return Convert.ToByte(score);
        }

        public void Dispose()
        {
            this._BASE = null;
            this._CONT = null;

            this.Minusculas = false;
            this.Mayusculas = false;
            this.Numeros = false;
            this.Especiales = false;
            this.Otros = null;
            this.ReOrdenamiento = ERandomSort.None;
        }
    }
}
#endif