namespace Codex.Struct
{
    public struct SColumn
    {
        public string Name;
        public bool IsPrimaryKey;
        public bool IsIdentity;
        public bool IsForeignKey;

        public SColumn(string _name, bool _pk = false, bool _id = false, bool _fk = false)
        {
            this.Name = _name;
            this.IsPrimaryKey = _pk;
            this.IsIdentity = _id;

            this.IsForeignKey = _fk;
        }
    }
}
