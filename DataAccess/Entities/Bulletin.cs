namespace DataAccess.Entities;

public class Bulletin
{
    public int Id { get; set; }
    public string Title { get; set; }
    public BulletinType BulletinType { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int CityId { get; set; }
    public string? OwnerId { get; set; }
    public string Contacts { get; set; }
    public string? Description { get; set; }

    // ---------- navigation properties
    public Category? Category { get; set; }
    public City? City { get; set; }
    public User? Owner { get; set; }
    public ICollection<File>? Pictures { get; set; }

    public Bulletin()
    {
        Title = Contacts = string.Empty;
    }
}

public enum BulletinType
{
    None = 0,
    Product,
    Voluntary, // Giveaways or asking for help
    Vacancy,
    Swap
}
