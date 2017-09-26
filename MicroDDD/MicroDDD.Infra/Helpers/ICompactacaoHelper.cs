using System.IO;
using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public interface ICompactacaoHelper
    {
        Task<byte[]> CompactaGzip(byte[] dados);
        Task<byte[]> DescompactaGzipAsync(byte[] gzip);
        Task<MemoryStream> DescompactaGzipMemoryStream(byte[] gzip);
        Task<MemoryStream> DescompactaGzipMemoryStreamAsync(byte[] gzip);
    }
}