namespace M101DotNet.Data.Common.Contracts
{
    public interface IDatabaseProvider<T>
    {
        T Create();
    }
}