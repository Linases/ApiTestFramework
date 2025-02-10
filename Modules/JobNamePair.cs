namespace Modules;

public class JobNamePair
{
    public string Job { get; set; }
    public string Name { get; set; }

    public JobNamePair(string name, string job)
    {
        Name = name;
        Job = job;
    }

    protected JobNamePair()
    {
    }
}