using Newtonsoft.Json;

namespace Modules;

public class EmailPasswordPair
{
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("password")]
    public string? Password { get; set; }

    public EmailPasswordPair(string email, string? password)
    {
        Email = email;
        Password = password;
    }

    protected EmailPasswordPair()
    {
    }
}