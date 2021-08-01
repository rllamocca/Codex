/*
ESTO NO DEBERIA ESTAR AQUI

        static string ToStringBits(byte[] _array)
        {
            if (_array == null)
                return null;

            string _return = null;
            for (uint _i = 0; _i < _array.Length; _i++)
                _return += string.Format("[{0}] -> {1}\n", _i, Convert.ToString(_array[_i], 2).PadLeft(8, '0'));

            return _return;
        }
        static string ToStringBytes(byte[] _array)
        {
            if (_array == null)
                return null;

            string _return = null;
            for (uint _i = 0; _i < _array.Length; _i++)
                _return += string.Format("[{0}] -> {1}\n", _i, _array[_i]);

            return _return;
        }

namespace Codex.Helper
{
    public static class StringHelper
    {
        private static String Formato_Rut(String _a)
        {
            if (_a == null) return null;
            _a = _a.Replace(".", "").Replace("-", "");
            if (_a.Length == 0) return null;

            List<Char> _l = new List<Char>();
            List<Char> _lr = _a.Reverse().ToList();
            for (Int32 _n = 0; _n < _lr.Count; ++_n)
            {
                _l.Add(_lr[_n]);
                if (_n == 0)
                    _l.Add('-');
                else if (_n % 3 == 0 && _n != _lr.Count - 1)
                    _l.Add('.');
            }
            _l.Reverse();
            return String.Join("", _l);
        }

        public static Task Write(ref Stream _a, string _path, CancellationToken _token = default)
        {
            return Write(_a, _path, _token);
        }
    }
}
*/


/*
using System;
using System.Text;
using System.Threading;

/// <summary>
/// An ASCII progress bar
/// </summary>
public class ProgressBar : IDisposable, IProgress<double>
{
    private const int _BLOCKCOUNT = 50;
    private const string _ANIMATION = @"|/-\";

    private readonly Timer _TIMER;
    private readonly TimeSpan _ANIMATIONINTERVAL = TimeSpan.FromSeconds(1.0 / 8);

    private double _CURRENTPROGRESS = 0;
    private string _CURRENTTEXT = string.Empty;
    private bool _DISPOSED = false;
    private int _ANIMATIONINDEX = 0;

    public ProgressBar()
    {
        _TIMER = new Timer(TimerHandler);

        if (Console.IsOutputRedirected == false)
            ResetTimer();
    }

    public void Report(double value)
    {
        // Make sure value is in [0..1] range
        value = Math.Max(0, Math.Min(1, value));
        Interlocked.Exchange(ref _CURRENTPROGRESS, value);
    }

    private void TimerHandler(object state)
    {
        lock (_TIMER)
        {
            if (_DISPOSED) return;

            int progressBlockCount = (int)(_CURRENTPROGRESS * _BLOCKCOUNT);
            int percent = (int)(_CURRENTPROGRESS * 100);
            string text = string.Format("[{0}{1}] {2,3}% {3}",
                new string('#', progressBlockCount),
                new string('-', _BLOCKCOUNT - progressBlockCount),
                percent,
                _ANIMATION[_ANIMATIONINDEX++ % _ANIMATION.Length]);
            UpdateText(text);

            ResetTimer();
        }
    }

    private void UpdateText(string text)
    {
        // Get length of common portion
        int commonPrefixLength = 0;
        int commonLength = Math.Min(_CURRENTTEXT.Length, text.Length);
        while (commonPrefixLength < commonLength && text[commonPrefixLength] == _CURRENTTEXT[commonPrefixLength])
        {
            commonPrefixLength++;
        }

        // Backtrack to the first differing character
        StringBuilder outputBuilder = new StringBuilder();
        outputBuilder.Append('\b', _CURRENTTEXT.Length - commonPrefixLength);

        // Output new suffix
        outputBuilder.Append(text.Substring(commonPrefixLength));

        // If the new text is shorter than the old one: delete overlapping characters
        int overlapCount = _CURRENTTEXT.Length - text.Length;
        if (overlapCount > 0)
        {
            outputBuilder.Append(' ', overlapCount);
            outputBuilder.Append('\b', overlapCount);
        }

        Console.Write(outputBuilder);
        _CURRENTTEXT = text;
    }

    private void ResetTimer()
    {
        _TIMER.Change(_ANIMATIONINTERVAL, TimeSpan.FromMilliseconds(-1));
    }

    public void Dispose()
    {
        lock (_TIMER)
        {
            _DISPOSED = true;
            UpdateText(string.Empty);
        }
    }
}
 */


/*
        private static double Aparicion(byte napa, byte nmax)
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
        public static byte Valorar(string password)
        {
            double score = 0.0;
            if (password.Length != 0)
            {
                Match m;
                byte con;
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
                foreach (char item in ReadOnly._LOWERCASE)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.Aparicion(con, por);
                con = 0;
                foreach (char item in ReadOnly._UPPERCASE)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.Aparicion(con, por);
                con = 0;
                foreach (char item in ReadOnly._NUMBERS)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.Aparicion(con, por);
                con = 0;
                foreach (char item in ReadOnly._SPECIALS)
                {
                    if (password.Contains(item))
                        con++;
                }
                score += Password.Aparicion(con, por);
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
*/

/*
// Creates and initializes a new Queue.
Queue myQ = new Queue();
myQ.Enqueue("The");
myQ.Enqueue("quick");
myQ.Enqueue("brown");
myQ.Enqueue("fox");

// Displays the Queue.
Console.Write("Queue values:");
PrintValues(myQ);

// Removes an element from the Queue.
Console.WriteLine("(Dequeue)\t{0}", myQ.Dequeue());

// Displays the Queue.
Console.Write("Queue values:");
PrintValues(myQ);

// Removes another element from the Queue.
Console.WriteLine("(Dequeue)\t{0}", myQ.Dequeue());

// Displays the Queue.
Console.Write("Queue values:");
PrintValues(myQ);

// Views the first element in the Queue but does not remove it.
Console.WriteLine("(Peek)   \t{0}", myQ.Peek());

// Displays the Queue.
Console.Write("Queue values:");
PrintValues(myQ);
*/

/*
        public static void EncodingInfo()
        {
            // Print the header.
            Console.Write("Info.CodePage      ");
            Console.Write("Info.Name                    ");
            Console.Write("Info.DisplayName");
            Console.WriteLine();

            // Display the EncodingInfo names for every encoding, and compare with the equivalent Encoding names.
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();

                Console.Write("{0,-15}", ei.CodePage);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.Write("{0,-25}", ei.Name);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.Write("{0,-25}", ei.DisplayName);
                if (ei.CodePage == e.CodePage)
                    Console.Write("    ");
                else
                    Console.Write("*** ");

                Console.WriteLine();
            }
        }
*/

/*

    public class CachedTimeSource
    {
        private int _LTC = -1;
        private DateTime _LDT = DateTime.MinValue;

        public DateTime FreshTime { get { return DateTime.Now; } }
        public DateTime Last
        {
            get
            {
                int _tc = Environment.TickCount;
                if (_tc == this._LTC)
                    return this._LDT;
                else
                {
                    DateTime _return = this.FreshTime;
                    this._LTC = _tc;
                    this._LDT = _return;
                    return _return;
                }
            }
        }
    }
*/

/*
            int _max = 10;
            int _max2 = 10;
            int _max3 = 10;
            int _max4 = 10;
            int _max5 = 10;

            ElapsedTime _et = new();
            ProgressBar32 _pb = new(_max, _et);
            ProgressBar32 _pb2 = new(_max2, _pb);
            ProgressBar32 _pb3 = new(_max3, _pb2);
            ProgressBar32 _pb4 = new(_max4, _pb3);
            ProgressBar32 _pb5 = new(_max5, _pb4);

            for (int _i = 1; _i <= _pb.Length; _i++)
            {
                Thread.Sleep(100);
                _pb.Report(_i);
            }
            for (int _i = 1; _i <= _pb2.Length; _i++)
            {
                Thread.Sleep(100);
                _pb2.Report(_i);
            }
            for (int _i = 1; _i <= _pb3.Length; _i++)
            {
                Thread.Sleep(100);
                _pb3.Report(_i);
            }
            for (int _i = 1; _i <= _pb4.Length; _i++)
            {
                Thread.Sleep(100);
                _pb4.Report(_i);
            }
            for (int _i = 1; _i <= _pb5.Length; _i++)
            {
                Thread.Sleep(100);
                _pb5.Report(_i);
            }

            _pb5.Dispose();
            _pb4.Dispose();
            _pb3.Dispose();
            _pb2.Dispose();
            _pb.Dispose();
            _et.Dispose();
 */

/*
using NPOI.SS.UserModel;
public static class NpoiExtensions
{
    public static string GetFormattedCellValue(this ICell cell, IFormulaEvaluator eval = null)
    {
        if (cell != null)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;

                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        DateTime date = cell.DateCellValue;
                        ICellStyle style = cell.CellStyle;
                        // Excel uses lowercase m for month whereas .Net uses uppercase
                        string format = style.GetDataFormatString().Replace('m', 'M');
                        return date.ToString(format);
                    }
                    else
                    {
                        return cell.NumericCellValue.ToString();
                    }

                case CellType.Boolean:
                    return cell.BooleanCellValue ? "TRUE" : "FALSE";

                case CellType.Formula:
                    if (eval != null)
                        return GetFormattedCellValue(eval.EvaluateInCell(cell));
                    else
                        return cell.CellFormula;

                case CellType.Error:
                    return FormulaError.ForInt(cell.ErrorCellValue).String;
            }
        }
        // null or blank cell, or unknown cell type
        return string.Empty;
    }
}
 */