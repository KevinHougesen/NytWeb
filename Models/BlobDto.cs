namespace NytWeb.Models;

public class BlobDto
{
    public string? Uri { get; set; }
    public string? Name { get; set; }
    public string? Id { get; set; }
    public string? ContentType { get; set; }
    public Stream? Content { get; set; }
}