namespace Codex
{
    public class ProgressState
    {
        public int Length { set; get; }
        public int Value { set; get; }
        public string Message { set; get; }

        public decimal Percentage()
        {
            return U221EHelper.Division(1.0m * this.Value, this.Length);
        }
        public string Proportion()
        {
            return string.Format("{0}/{1}", this.Value, this.Length);
        }
    }
}
