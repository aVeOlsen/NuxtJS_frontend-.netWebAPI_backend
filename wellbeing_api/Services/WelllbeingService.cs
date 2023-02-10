using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using wellbeing_api.Data;
using wellbeing_api.Models;

namespace wellbeing_api.Services;

public class WellbeingService:IDatabaseService<Wellbeing>
{
    private readonly IMongoCollection<Wellbeing> _wellbeingCollection;

    public WellbeingService(
        IOptions<ApplicationDbContext> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);
        _wellbeingCollection = mongoDatabase.GetCollection<Wellbeing>(databaseSettings.Value.WellbeingCollection);

    }
    public async Task CreateAsync(List<Wellbeing> newWellbeing)=>
        await _wellbeingCollection.InsertManyAsync(newWellbeing);

    public async Task<List<Wellbeing>> GetAllAsync() =>
        await _wellbeingCollection.Find(_ => true).ToListAsync();
    public async Task<Wellbeing?> GetAsync(Guid id) =>
        await _wellbeingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task<List<Wellbeing>> GetQuistionnaireAsync(string id) =>
        await _wellbeingCollection.Find(x => x.QuestionId == id).ToListAsync();
    public async Task<List<Wellbeing>> GetDepartmentAsync(string id) =>
        await _wellbeingCollection.Find(x => x.DepartmentTitle == id).ToListAsync();
    public async Task<List<Wellbeing>> GetUserAsync(Guid id) =>
        await _wellbeingCollection.Find(x => x.UserID == id).ToListAsync();
    public async Task RemoveAsync(Guid id) =>
        await _wellbeingCollection.DeleteOneAsync(x => x.Id == id);

    public async Task UpdateAsync(Guid id, Wellbeing updatedWellbeing) =>
        await _wellbeingCollection.ReplaceOneAsync(x => x.Id == id, updatedWellbeing);

    public async Task SetPrimeAsync(string id)
    {
        var builder = Builders<Wellbeing>.Update;
        await _wellbeingCollection.UpdateManyAsync<Wellbeing>(x => x.QuestionId == id, builder.Set("Primary", true));
        await _wellbeingCollection.UpdateManyAsync<Wellbeing>(x => x.QuestionId != id, builder.Set("Primary", false));
    }


}
