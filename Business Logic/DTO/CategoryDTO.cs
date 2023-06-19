using DataAccess.Entities;

namespace Business_Logic.DTO;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
