using Newtonsoft.Json;

namespace Modules;

public class CreateUserVm: JobNamePairVm
{
    public DateTime CreatedAt { get; set; }
    public int Id { get; set; }

}

