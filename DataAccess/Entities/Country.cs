using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Country
	{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<City> Cities { get; set; }
    [NotMapped] // Needs testing
    public ICollection<Bulletin> Products
    {
        get
        {
            List<Bulletin> products = new();
            foreach (City city in Cities)
                products.AddRange(city.Products.ToList());
            return products;
        }
    }

    public Country()
    {
        Name = string.Empty;
        Cities = new List<City>();
    }
}