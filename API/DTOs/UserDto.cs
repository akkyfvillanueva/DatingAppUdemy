using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class UserDto
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}