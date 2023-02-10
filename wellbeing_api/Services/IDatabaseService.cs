using wellbeing_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using wellbeing_api.Data;

namespace wellbeing_api.Services;

interface IDatabaseService<TUser>
{
    Task<List<TUser>> GetAllAsync();


    Task<TUser?> GetAsync(Guid id);

    Task UpdateAsync(Guid id, TUser updatedUser);

    Task RemoveAsync(Guid id);
}