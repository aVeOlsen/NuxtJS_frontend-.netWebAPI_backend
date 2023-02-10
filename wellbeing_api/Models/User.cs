using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace wellbeing_api.Models
{
    [CollectionName("worker")]
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("username")]
        private string? username;
        public string UserName { get => username = FirstName + ' ' + LastName; }
        [BsonElement("firstName")]
        public string? FirstName { get; set; }
        [BsonElement("lastName")]
        public string? LastName { get; set; }
        [BsonElement("mail")]
        public string? Mail { get; set; }
        public string? Email { get; set; }
        [BsonElement("role")]
        public Role? role { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("departmentTitle")]
        public string? DepartmentTitle { get; set; }


    }
    public enum Role
    {
        Admin = 0,
        TeamLeader = 1,
        TeamWorker = 2,
    }
}