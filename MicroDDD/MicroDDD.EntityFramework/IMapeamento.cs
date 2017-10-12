using MicroDDD.Dominio.Entidade;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroDDD.EntityFramework
{
    public interface IMapeamento<T> where T : class, IEntidade
    {
        void Configura(EntityTypeBuilder<T> typeBuilderEntidade);
    }
}
