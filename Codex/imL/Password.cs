#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Enum;
using Codex.Helper;

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codex
{
    public class Password : IDisposable
    {
        private const String _MI = "abcdefghijklmnñopqrstuvwxyz";
        private const String _MA = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        private const String _NU = "1234567890";
        private const String _ES = " |°¬#$%&=',;.:¨*+~-_^`´()[]{}<>¡!¿?/\\\"";
        //؟

        #region PROPIEDADES
        private Char[] _BASE;
        private String _CONT = String.Empty;

        public Boolean Minusculas { set; get; } = true;
        public Boolean Mayusculas { set; get; } = true;
        public Boolean Numeros { set; get; } = true;
        public Boolean Especiales { set; get; } = true;
        public String Otros { set; get; } = "";

        public Char[] Base { get { return this._BASE; } }
        public ERandomSort ReOrdenamiento { set; get; } = ERandomSort.Fisher_Yates;
        public String Contrasena { get { return this._CONT; } }
        #endregion

        #region METODOS OBJETO
        public Return Generar(UInt16 _largo = 8)
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
        #endregion

        #region METODOS ESTATICOS
        public static Char[] PrepararBase(Boolean _mi, Boolean _ma, Boolean _nu, Boolean _es, String _otros = "")
        {
            String _return = String.Empty;
            if (_mi) _return += Password._MI;
            if (_ma) _return += Password._MA;
            if (_nu) _return += Password._NU;
            if (_es) _return += Password._ES;
            if (_otros.Length > 0) _return += _otros;
            _return = _return.Clean_TBLFCR();
            return _return.ToArray().Distinct().ToArray();
        }
        public static Char[] ReOrdenar(Char[] _data, ERandomSort _s = ERandomSort.Fisher_Yates)
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
        public static String Generar(Char[] _base, UInt16 _largo = 8)
        {
            Char[] _tmp = new Char[_largo];
            Random _rdm = new Random();
            for (UInt16 _i = 0; _i < _largo; _i++)
                _tmp[_i] = _base[_rdm.Next(0, _base.Length)];

            String[] _return = new String[_largo];
            for (UInt16 _i = 0; _i < _largo; _i++)
                _return[_i] = Convert.ToString(_tmp[_i]);

            return String.Join("", _return);
        }
        private static Double aparicion(UInt16 napa, UInt16 nmax)
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
        public static Byte valorar(String password)
        {
            Double score = 0.0;
            if (password.Length != 0)
            {
                Match m;
                UInt16 con = 0;
                UInt16 por = (UInt16)(password.Length * 0.25 + 0.5);
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
                foreach (Char item in _MI)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (Char item in _MA)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (Char item in _NU)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.aparicion(con, por);
                con = 0;
                foreach (Char item in _ES)
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
        #endregion
    }
}

#endif