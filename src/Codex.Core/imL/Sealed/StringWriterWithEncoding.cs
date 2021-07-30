using System.IO;
using System.Text;

namespace Codex.Sealed
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _ENCODING;

        public StringWriterWithEncoding(Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;
            this._ENCODING = _enc;
        }

        public override Encoding Encoding
        {
            get { return this._ENCODING; }
        }
    }
}
