namespace ScreenSound.API.Requests
{
    public record MusicaRequestEdit(int id, string nome, int artistaId, int? anoLancamento) : MusicaRequest(nome, anoLancamento);
}
