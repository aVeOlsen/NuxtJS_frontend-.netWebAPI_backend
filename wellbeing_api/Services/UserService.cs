using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using wellbeing_api.Data;
using wellbeing_api.Models;

namespace wellbeing_api.Services;

public class UserService
{

    private readonly IMongoCollection<User> _userCollection;

    private readonly string? key;
    public UserService(
        IOptions<ApplicationDbContext> databaseSettings, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);
        _userCollection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UserCollection);

        this.key = configuration.GetSection("JwtKey").ToString();
    }
    public async Task<List<User>> GetAllAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(Guid id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(Guid id, User updateduser) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updateduser);

    public async Task RemoveAsync(Guid id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);

    // public Task GeUserAsync(string email, string password)=>
    //      _userCollection.Find(x => x.Mail == email && x.Password == password).FirstOrDefaultAsync();
    public ApplicationUser Authenticate(string email, string password)
    {
        var user = this._userCollection.Find(x => x.Mail.ToLower() == email.ToLower() && x.Password == password).FirstOrDefault();
        if (user == null)
            return null;
        ApplicationUser appUser = new ApplicationUser();

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
        var _token= tokenHandler.WriteToken(maker);
        appUser.Token=_token;

        return appUser;
    }

}