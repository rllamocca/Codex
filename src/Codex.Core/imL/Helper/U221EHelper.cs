/*
sbyte
byte
short
ushort
int
uint
long
ulong
nint
nuint

float
double
decimal

*/

namespace Codex
{
    public static class U221EHelper
    {
        #region Division
        public static sbyte Division(sbyte _top, sbyte _bot, sbyte _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (sbyte)(_top / _bot);
        }
        public static byte Division(byte _top, byte _bot, byte _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (byte)(_top / _bot);
        }
        public static short Division(short _top, short _bot, short _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (short)(_top / _bot);
        }
        public static ushort Division(ushort _top, ushort _bot, ushort _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (ushort)(_top / _bot);
        }
        public static int Division(int _top, int _bot, int _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (_top / _bot);
        }
        public static uint Division(uint _top, uint _bot, uint _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (_top / _bot);
        }
        public static long Division(long _top, long _bot, long _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (_top / _bot);
        }
        public static ulong Division(ulong _top, ulong _bot, ulong _u221e = 0)
        {
            if (_top == 0) return 0;
            if (_bot == 0) return _u221e;
            return (_top / _bot);
        }
        //#if NETSTANDARD1_0 ||NETSTANDARD1_1|| NETSTANDARD1_2 ||NETSTANDARD1_3 ||NETSTANDARD2_0
        //        public static nint Division(nint _dividend, nint _divisor, nint _u221e = 0)
        //        {
        //if (_divisor == 0) return _u221e;
        //if (_dividend == 0) return 0;
        //            return (_dividend / _divisor);
        //        }
        //        public static nuint Division(nuint _dividend, nuint _divisor, nuint _u221e = 0)
        //        {
        //if (_divisor == 0) return _u221e;
        //if (_dividend == 0) return 0;
        //            return (_dividend / _divisor);
        //        }
        //#endif

        public static float Division(float _top, float _bot, float _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (float)(_top / _bot);
        }
        public static double Division(double _top, double _bot, double _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (double)(_top / _bot);
        }
        public static decimal Division(decimal _top, decimal _bot, decimal _u221e = 0)
        {
            if (_bot == 0) return _u221e;
            if (_top == 0) return 0;
            return (_top / _bot);
        }
        #endregion

        //public static sbyte Subtraction(sbyte _left, sbyte _right, sbyte _u221e = 0)
        //{
        //    sbyte _tmp = (sbyte)(_left - _right);
        //    if (_tmp >= _u221e) return _tmp;
        //    return (sbyte)(_left - _u221e);
        //}
    }
}
