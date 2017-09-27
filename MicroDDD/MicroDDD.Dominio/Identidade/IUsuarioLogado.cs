namespace MicroDDD.Dominio.Identidade
{
    /// <summary>
    /// Define propriedades de um usuário logado
    /// </summary>
    public interface IUsuarioLogado
    {
        /// <summary>
        /// Identificador do usuário
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// Login ou código do usuário
        /// </summary>
        string Login { get; set; }
    }
}
