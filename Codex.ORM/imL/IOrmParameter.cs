namespace Codex.ORM
{
    public interface IOrmParameter
    {
        string Source { set; get; }
        object Value { set; get; }
        string Affect { set; get; }
    }
}
