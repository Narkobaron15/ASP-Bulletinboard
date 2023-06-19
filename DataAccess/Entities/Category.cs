namespace DataAccess.Entities;

public class Category
	{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // ---------- navigation properties
    public ICollection<Bulletin>? Products { get; set; }

    public override string ToString() => Name;
}
