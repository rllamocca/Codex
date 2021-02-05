namespace Codex.Struct
{
    public struct STable
    {
        public string Name;
        public SColumn[] Columns;

        public STable(string _name, SColumn[] _cols = null)
        {
            this.Name = _name;
            this.Columns = _cols;
        }
    }
}
