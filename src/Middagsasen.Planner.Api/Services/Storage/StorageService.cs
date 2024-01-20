using Azure.Storage.Blobs;
using System.Net;

namespace Middagsasen.Planner.Api.Services.Storage
{
    public interface IStorageService
    {

    }

    public class BlobStorageService : IStorageService
    {
        protected string ConnectionString { get; set; }
        protected string Container { get; set; }

        public BlobStorageService(string connectionString, string container)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            ConnectionString = connectionString;
            Container = container;
        }

        public async Task Save(string fileName, Stream data)
        {
            var client = new BlobClient(ConnectionString, Container, fileName);
            await client.UploadAsync(data);
        }

        public async Task Save(string fileName, Stream data, bool overwrite)
        {
            var client = new BlobClient(ConnectionString, Container, fileName);
            await client.UploadAsync(data, overwrite: overwrite);
        }

        public async Task Save(string fileName, byte[] data)
        {
            var client = new BlobClient(ConnectionString, Container, fileName);
            var binaryData = new BinaryData(data);
            await client.UploadAsync(binaryData);
        }

        public async Task<byte[]> Read(string fileName)
        {
            var client = new BlobClient(ConnectionString, Container, fileName);
            using (var ms = new MemoryStream())
            {
                await client.DownloadToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<IEnumerable<string>> List(string path)
        {
            var client = new BlobContainerClient(ConnectionString, Container);
            var pages = client.GetBlobsAsync(Azure.Storage.Blobs.Models.BlobTraits.None, Azure.Storage.Blobs.Models.BlobStates.None, path).AsPages();
            var result = new List<string>();
            //await foreach (var blob in blobs)
            //{
            //    result.Add(blob.Name);
            //}

            var enumerator = pages.GetAsyncEnumerator(); // You may also want a CancellationToken here
            try
            {
                while (await enumerator.MoveNextAsync()) // You may also want .ConfigureAwait(false) or a CancellationToken here
                {
                    var blobs = enumerator.Current.Values;
                    foreach (var blob in blobs)
                    {
                        result.Add(blob.Name);
                    }
                }
            }
            finally
            {
                await enumerator.DisposeAsync(); // You may also want .ConfigureAwait(false) here
            }
            return result;
        }

        public async Task Delete(string fileName)
        {
            var client = new BlobClient(ConnectionString, Container, fileName);
            await client.DeleteIfExistsAsync();
        }
    }
}
