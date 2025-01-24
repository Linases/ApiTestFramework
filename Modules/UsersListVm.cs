using Newtonsoft.Json;

namespace Modules;

public class UsersListVm
{
    public int Page { get; set; }
    [JsonProperty("per_page")]
    public int PerPage { get; set; }
    public int Total { get; set; }
    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }
    public List<UserVm> Data { get; set; }
    public SupportFieldVm Support { get; set; }
}