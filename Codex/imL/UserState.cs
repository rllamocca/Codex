using Codex.Helper;

namespace Codex
{
    public class UserState
    {
        public int Distance { set; get; }
        public int Position { set; get; }
        public string Message { set; get; }

        public decimal Percentage()
        {
            return U221EHelper.Division(1.0m * this.Position, this.Distance);
        }
        public string Proportion()
        {
            return string.Format("{0}/{1}", this.Position, this.Distance);
        }
    }
}
