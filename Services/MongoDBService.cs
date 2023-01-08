using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService {

    private readonly IMongoCollection<Record> _RecordCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _RecordCollection = database.GetCollection<Record>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Record>> GetAsync() {
        return await _RecordCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task CreateAsync(Record Record) {
        await _RecordCollection.InsertOneAsync(Record);
        return;
    }
    public async Task AddToPlaylistAsync(string id, string movieId) {
        FilterDefinition<Record> filter = Builders<Record>.Filter.Eq("Id", id);
        UpdateDefinition<Record> update = Builders<Record>.Update.AddToSet<string>("movieIds", movieId);
        await _RecordCollection.UpdateOneAsync(filter, update);
        return;
    }

}
