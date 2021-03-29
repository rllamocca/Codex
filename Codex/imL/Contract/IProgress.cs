namespace Codex.Contract
{
#if (NET35 || NET40)
    public interface IProgress<in T>
    {
        void Report(T value);
    }
#endif
}