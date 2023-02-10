using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using wellbeing_api.Data;
using wellbeing_api.Models;

namespace wellbeing_api.Services;

public class DepartmentService
{
        private readonly IMongoCollection<Department> _departmentCollection;
        public DepartmentService(
        IOptions<ApplicationDbContext> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);
        _departmentCollection = mongoDatabase.GetCollection<Department>(databaseSettings.Value.DepartmentCollection);

    }
    public async Task CreateAsync(Department newDepartment)=>
        await _departmentCollection.InsertOneAsync(newDepartment);

    public async Task<List<Department>> GetAllAsync()=>
        await _departmentCollection.Find(_=>true).ToListAsync();

    public async Task<Department?> GetAsync(string title)=>
        await _departmentCollection.Find(x=>x.Title==title).FirstOrDefaultAsync();

    public async Task RemoveAsync(string title)=>
        await _departmentCollection.DeleteOneAsync(x=>x.Title==title);

    public async Task UpdateAsync(string title, Department updatedDepartment)=>
        await _departmentCollection.ReplaceOneAsync(x=>x.Title==title, updatedDepartment);
}
