using Codex.Utility;

using System.IO;
using System.Text;

namespace Codex.Sealed
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _ENCODING;

        public StringWriterWithEncoding(Encoding _enc = null)
        {
            EncodingUtility.SolutionDefault(ref _enc);

            this._ENCODING = _enc;
        }

        public override Encoding Encoding
        {
            get { return this._ENCODING; }
        }
    }
}
