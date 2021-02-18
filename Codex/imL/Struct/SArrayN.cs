namespace Codex.Struct
{
    public struct SArray1<T>
    {
        public T Value { get; }

        public SArray1(T _value)
        {
            this.Value = _value;
        }
    }

    public struct SArray2<T, T1>
    {
        public T A { get; }
        public T1 Value { get; }

        public SArray2(T _a, T1 _value)
        {
            this.A = _a;
            this.Value = _value;
        }
    }

    public struct SArray3<T, T1, T2>
    {
        public T A { get; }
        public T1 B { get; }
        public T2 Value { get; }

        public SArray3(T _a, T1 _b, T2 _value)
        {
            this.A = _a;
            this.B = _b;
            this.Value = _value;
        }
    }

    public struct SArray4<T, T1, T2, T3>
    {
        public T A { get; }
        public T1 B { get; }
        public T2 C { get; }
        public T3 Value { get; }

        public SArray4(T _a, T1 _b, T2 _c, T3 _value)
        {
            this.A = _a;
            this.B = _b;
            this.C = _c;
            this.Value = _value;
        }
    }

    public struct SArray5<T, T1, T2, T3, T4>
    {
        public T A { get; }
        public T1 B { get; }
        public T2 C { get; }
        public T3 D { get; }
        public T4 Value { get; }

        public SArray5(T _a, T1 _b, T2 _c, T3 _d, T4 _value)
        {
            this.A = _a;
            this.B = _b;
            this.C = _c;
            this.D = _d;
            this.Value = _value;
        }
    }
}
