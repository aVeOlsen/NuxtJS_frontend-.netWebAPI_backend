using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
 
namespace wellbeing_api.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole
    {
 
    }
}