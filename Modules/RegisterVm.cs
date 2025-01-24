namespace Modules;

public class RegisterVm : EmailPasswordPair
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string Error { get; set; }
}