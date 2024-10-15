namespace AcervoFilmesAPI.Models;

public class Filme
{
    public Filme()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string? Nome { get; set; }
    public int Nota { get; set; }
    public string? Diretor { get; set; }
}