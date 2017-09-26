using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public class CompactacaoHelper : ICompactacaoHelper
    {
        public async Task<byte[]> DescompactaGzipAsync(byte[] gzip)
        {
            using (var outStream = new MemoryStream())
            {
                using (var tinyStream = new GZipStream(outStream, CompressionMode.Compress))
                using (var stream = new MemoryStream(gzip))
                {
                    await stream.CopyToAsync(tinyStream).ConfigureAwait(false);
                }
                return outStream.ToArray();
            }
        }

        public async Task<MemoryStream> DescompactaGzipMemoryStream(byte[] gzip)
        {
            return new MemoryStream(await DescompactaGzipAsync(gzip));
        }

        public async Task<MemoryStream> DescompactaGzipMemoryStreamAsync(byte[] gzip)
        {
            return await DescompactaGzipMemoryStream(gzip);
        }

        public async Task<byte[]> CompactaGzip(byte[] dados)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    await gzip.WriteAsync(dados, 0, dados.Length).ConfigureAwait(false);
                }
                return memory.ToArray();
            }
        }
    }
}
