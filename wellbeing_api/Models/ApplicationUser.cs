using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("user")]
    public class ApplicationUser : MongoIdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Token {get;set;}
        public string? DepartmentTitle { get; set; }
        public Role? Role { get; set; }

        public ApplicationUser() : base()
        { 
            
        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
    }
}