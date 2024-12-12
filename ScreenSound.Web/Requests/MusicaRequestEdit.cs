namespace ScreenSound.Web.Requests
{
    public record MusicaRequestEdit(int id, string nome, int artistaId, int? anoLancamento, ICollection<GeneroRequest> generos = null) : MusicaRequest(nome, anoLancamento, artistaId, generos);
}
