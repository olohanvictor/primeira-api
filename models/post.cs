namespace primeira_api.Models;

public class Post
{
    public int Id { get; set; }
    public int IdAutor { get; set; }
    public required string Conteudo { get; set; }
}
