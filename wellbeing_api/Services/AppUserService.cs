using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using wellbeing_api.Data;
using wellbeing_api.Models;

namespace wellbeing_api.Services;
public class AppUserService:IDatabaseService<ApplicationUser>
{
    private readonly IMongoCollection<ApplicationUser> _userCollection;
    private readonly string? key;
    public AppUserService(
        IOptions<ApplicationDbContext> databaseSettings, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<ApplicationUser>(
            databaseSettings.Value.ApplicationUserCollection);
        this.key = configuration.GetSection("JwtKey").ToString();
    }

    public async Task<List<ApplicationUser>> GetAllAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<ApplicationUser?> GetAsync(Guid Id) =>
        await _userCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

    public async Task CreateAsync(ApplicationUser ApplicationUser) =>
        await _userCollection.InsertOneAsync(ApplicationUser);

    public async Task RemoveAsync(Guid Id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == Id);

    public async Task UpdateAsync(Guid id, ApplicationUser updatedUser)
    {
        await _userCollection.ReplaceOneAsync(x=>x.Id==id, updatedUser);
    }
    public string Authenticate(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
            }),
            Expires = DateTime.Now.AddDays(1),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature
            )
        };
        var maker = tokenHandler.CreateToken(tokenDescriptor);
        var _token = tokenHandler.WriteToken(maker);

        return _token;
    }
}