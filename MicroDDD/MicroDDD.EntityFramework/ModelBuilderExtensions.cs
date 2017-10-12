using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace MicroDDD.EntityFramework
{
    public static class ModelBuilderExtensions 
    {
        public static void ConfigurarTodos(this ModelBuilder modelBuilder)
        {
            var mapeamentos = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.Name == "IMapeamento`1"))
                .Select(x => Activator.CreateInstance(x, new object[] { })).ToList();

            foreach (var mapeamento in mapeamentos)
            {
                MethodInfo configura = mapeamento.GetType().GetMethod("Configura");
                Type tipoEntidade = configura.GetParameters()[0].ParameterType.GenericTypeArguments[0];


                MethodInfo[] methods = modelBuilder.GetType().GetMethods();
                MethodInfo method = null;
                foreach (var item in methods)
                {
                    if (item.Name == "Entity" && item.IsGenericMethod)
                    {
                        method = item;
                        break;
                    }
                }
                if (method == null)
                    break;

                var metodoEntityTypeBuilder = method.MakeGenericMethod(tipoEntidade);
                var typeBuilderEntidade = metodoEntityTypeBuilder.Invoke(modelBuilder, null);
                configura.Invoke(mapeamento, new object[] { typeBuilderEntidade });
            }
        }
    }
}
