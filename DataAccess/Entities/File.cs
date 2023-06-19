namespace DataAccess.Entities;

public class File
{
    public int Id { get; set; }
    public string Url { get; set; }  = string.Empty;
    public string Name { get; set; }  = string.Empty;
    public int BulletinId { get; set; }

    public Bulletin? Bulletin { get; set; }
}
