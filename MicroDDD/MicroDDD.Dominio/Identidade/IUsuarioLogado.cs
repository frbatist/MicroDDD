namespace MicroDDD.Dominio.Identidade
{
    public interface IUsuarioLogado
    {
        long Id { get; set; }
        string Login { get; set; }
    }
}
