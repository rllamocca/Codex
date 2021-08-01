#if (NET35 || NET40) == false
using System.Threading.Tasks;
#endif

using System;
using System.Threading;

namespace Codex.Utility
{
    public static class TestHelper
    {
        private static int MAX_INT(int _max)
        {
            _max++;
            Random _r = new Random();
            _max = _r.Next(_max);

            return _max;
        }
        private static long MAX_LONG(long _max)
        {
            _max++;
            byte[] _bytes = BitConverter.GetBytes(_max);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(_bytes);
            Random _r = new Random();
            _r.NextBytes(_bytes);
            _max = BitConverter.ToInt64(_bytes, 0);

            return _max;
        }

        public static void RandomSleep(int _max)
        {
            _max = MAX_INT(_max);

            if (_max > 0)
                Thread.Sleep(_max);
        }
        public static void RandomSleep(TimeSpan _time)
        {
            long _max = MAX_LONG(_time.Ticks);

            if (_max > 0)
                Thread.Sleep(new TimeSpan(_max));
        }

#if (NET35 || NET40) == false
        public async static Task RandomDelay(int _max, CancellationToken _token = default)
        {
            _max = MAX_INT(_max);

            if (_max > 0)
                await Task.Delay(_max, _token);
        }
        public async static Task RandomDelay(TimeSpan _time, CancellationToken _token = default)
        {
            long _max = MAX_LONG(_time.Ticks);

            if (_max > 0)
                await Task.Delay(new TimeSpan(_max), _token);
        }
#endif
    }
}
