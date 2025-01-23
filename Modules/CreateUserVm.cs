using Newtonsoft.Json;

namespace Modules;

public class CreateUserVm: JobNamePair
{
    public DateTime CreatedAt { get; set; }
    public int Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}

