namespace Livros.Dominio.DTOs.Assunto;

public class AssuntoComLivroDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public bool PossuiLivrosAssociados { get; set; }
}