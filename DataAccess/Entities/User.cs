using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace DataAccess.Entities;

public class User : IdentityUser
{
    public DateTime Birthday { get; set; } = DateTime.Now;
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Bulletin> Bulletins { get; set; }

    public User()
    {
        Favorites = new Collection<Favorite>();
        Bulletins = new Collection<Bulletin>();
    }
}
