namespace M101DotNet.Data.Common.Contracts
{
    using MongoDB.Driver;

    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
    }
}