namespace Codex
{
    public class UserState
    {
        public int Distance { get; set; } = 0;
        public int Position { get; set; } = 0;
        public string Message { get; set; }

        public decimal Percentage()
        {
            if (this.Distance != 0.0m) return 1.0m * this.Position / this.Distance;
            return 0.0m;
        }
        public string Proportion()
        {
            return string.Format("{0} / {1}", this.Position, this.Distance);
        }
    }
}
