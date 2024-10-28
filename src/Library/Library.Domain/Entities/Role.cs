using Library.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;

public class Role : NamedEntity
{
    [JsonIgnore]
    public virtual List<User> Users { get; set; }

    public Role() { }

    public Role(string name) : this(Guid.NewGuid(), name, []) { }

    public Role(Guid id, string name, List<User> users)
    {
        Id = id;
        Name = name;
        Users = users;
    }
}
