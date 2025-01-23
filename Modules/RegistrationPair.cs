using Newtonsoft.Json;

namespace Modules;

public class RegistrationPair
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string? Password { get; set; }

    public RegistrationPair(string email, string? password)
    {
        Email = email;
        Password = password;
    }

    protected RegistrationPair()
    {
    }
}

