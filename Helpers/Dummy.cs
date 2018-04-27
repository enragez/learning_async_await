using System.IO;
using System.Threading.Tasks;

namespace Helpers
{
    public class Dummy
    {
        public void Convert()
        {
            ConvertAsync();
        }
        
        private async Task ConvertAsync()
        {
            var from = @"C:\Datastorage\source\data.txt";

            var data = await ReadDataAsync(from);

            var transformedData = TransformData(data);

            var saveTo = @"C:\Datastorage\transformeddata.txt";

            await WriteDataAsync(saveTo, transformedData);
        }

        private async Task<string> ReadDataAsync(string path)
        {
            using (var reader = File.OpenText(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private string TransformData(string data)
        {
            return string.Join(string.Empty, data.ToCharArray().Reverse());
        }

        private async Task WriteDataAsync(string saveToPath, string data)
        {
            using (var writer = File.CreateText(saveToPath))
            {
                await writer.WriteAsync(data);
            }
        }
    }
}