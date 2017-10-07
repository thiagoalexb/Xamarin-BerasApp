using Newtonsoft.Json;

namespace BerasApp.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
