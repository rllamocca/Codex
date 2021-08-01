using System.Text;

namespace Codex.Utility
{
    public static class EncodingUtility
    {
        public static void SolutionDefault(ref Encoding _enc)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;
        }
    }
}
