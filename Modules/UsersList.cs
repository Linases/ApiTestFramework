using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Modules;

public class UsersList
{
    public int Page { get; set; }
    [JsonProperty("per_page")]
    public int PerPage { get; set; }
    public int Total { get; set; }
    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }
    public List<User> Data { get; set; }

    public SupportField Support { get; set; }
}