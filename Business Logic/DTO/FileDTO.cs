namespace Business_Logic.DTO;

public class FileDTO
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int BulletinId { get; init; }

    public FileDTO(string Url = "", string Name = "", int BulletinId = 0)
    {
        this.Url = Url;
        this.Name = Name;
        this.BulletinId = BulletinId;
    }
}
