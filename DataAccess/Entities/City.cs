namespace DataAccess.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }

    public Country Country { get; set; }
    public ICollection<Bulletin> Products { get; set; }

    public override string ToString() => Name;

    public City()
    {
        Name = string.Empty;
        Products = new List<Bulletin>();
        Country = default!;
    }
}