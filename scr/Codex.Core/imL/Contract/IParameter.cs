namespace Codex.Contract
{
    public interface IParameter
    {
        string Source { set; get; }
        object Value { set; get; }
        string Affect { set; get; }
    }
}
