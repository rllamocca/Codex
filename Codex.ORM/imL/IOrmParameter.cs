namespace Codex.ORM
{
    public interface IOrmParameter
    {
        string Affect { set; get; }
        object Value { set; get; }
        string Name_Function { set; get; }
    }
}
