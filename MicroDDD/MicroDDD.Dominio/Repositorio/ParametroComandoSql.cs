using System.Data;

namespace MicroDDD.Dominio.Repositorio
{
    /// <summary>
    /// Parametro para comando SQL
    /// </summary>
    public class ParametroComandoSql
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Valor
        /// </summary>
        public object Valor { get; set; }
        /// <summary>
        /// Tipo
        /// </summary>
        public DbType Tipo { get; set; }
        /// <summary>
        /// Tamanho
        /// </summary>
        public int Tamanho { get; set; }
        /// <summary>
        /// Precisão
        /// </summary>
        public byte Precisao { get; set; }

        /// <summary>
        /// Construtor para campos obrigatorios
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="valor"></param>
        public ParametroComandoSql(string nome, object valor)
        {
            Nome = nome;
            Valor = valor;
        }
    }
}