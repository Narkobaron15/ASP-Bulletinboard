using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic.DTO;

public class BulletinDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required"), MinLength(3, ErrorMessage = "Must have at least three characters")]
    public string Title { get; set; }
    [Required, Range(1, 4, ErrorMessage = "Select the type")]
    public BulletinType BulletinType { get; set; }
    public int CategoryId { get; set; }
    [Range(0, (double)Decimal.MaxValue, ErrorMessage = "Price should be a number")]
    public decimal Price { get; set; }
    public int CityId { get; set; }
    [Required(ErrorMessage = "Specify your contacts"), MinLength(10, ErrorMessage = "This field must have at least ten characters")]
    public string Contacts { get; set; }
    public string? Description { get; set; }
    public List<FileDTO> Pictures { get; set; }

    public string? PrimaryImage => Pictures.FirstOrDefault()?.Url;
    public string FullPrice => Price.ToString("c");

    public BulletinDTO()
    {
        Title = Contacts = string.Empty;
        Pictures = new();
    }

    public override string ToString()
    {
        return Title;
    }
}
