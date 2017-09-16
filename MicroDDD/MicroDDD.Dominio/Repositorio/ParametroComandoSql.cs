using System.Data;

namespace MicroDDD.Dominio.Repositorio
{
    public class ParametroComandoSql
    {
        public string Nome { get; set; }
        public object Valor { get; set; }
        public DbType Tipo { get; set; }

        public ParametroComandoSql(string _nome, object _valor, DbType _tipo)
        {
            Nome = _nome;
            Valor = _valor;
            Tipo = _tipo;
        }
    }
}