namespace Modules;

public class RegistrationPair
{
    public string Email { get; set; }
    public string? Password { get; set; }

    public RegistrationPair(string email, string? password)
    {
        Email = email;
        Password = password;
    }

    public RegistrationPair(string email)
    {
        Email = email;
    }

    protected RegistrationPair()
    {
    }
}

