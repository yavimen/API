using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageServer.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Permission
    {
        User,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public Permission UserPermission { get; set; }   
    }
}
