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
        public async Task<Record> GetOneAsync(string id) {
        var filter = Builders<Record>.Filter.Eq("_id", ObjectId.Parse(id));

        var final = await _RecordCollection.Find(filter).SingleAsync();
        return final;
    }
    public async Task CreateAsync(Record Record) {
        await _RecordCollection.InsertOneAsync(Record);
        return;
    }

}
