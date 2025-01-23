using Newtonsoft.Json;

namespace Modules;

public class JobNamePairVm
{
    public string Job { get; set; }
    public string Name { get; set; }

    public JobNamePairVm(string name, string job)
    {
        Name = name;
        Job = job;
    }

    protected JobNamePairVm()
    {
    }
}
