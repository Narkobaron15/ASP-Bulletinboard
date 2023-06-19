namespace DataAccess.Entities;

public class Favorite
{
    public string UserId { get; set; }
    public int BulletinId { get; set; }

    // navigation properties
    public User? User { get; set; }
    public Bulletin? Bulletin { get; set; }

    public Favorite()
    {
        this.UserId = string.Empty;
    }
    public Favorite(string UserId, int BulletinId)
    {
        this.UserId = UserId;
        this.BulletinId = BulletinId;
    }
}
