namespace Codex
{
    public interface IOrmParameter
    {
        string Source { set; get; }
        object Value { set; get; }
        string Affect { set; get; }
    }
}
