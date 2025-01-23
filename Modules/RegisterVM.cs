namespace Modules;

public class RegisterVM : RegistrationPair
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string Error { get; set; }
}