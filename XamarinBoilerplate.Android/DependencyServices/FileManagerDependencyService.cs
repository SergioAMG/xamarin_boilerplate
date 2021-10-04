using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.Droid.DependencyServices;
using System.IO;
using System.Threading.Tasks;
using System;

[assembly: Dependency(typeof(FileManagerDependencyService))]
namespace XamarinBoilerplate.Droid.DependencyServices
{
    public class FileManagerDependencyService : IFileManager
    {
        public string PersonalFolderPath
        {
            get
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            }
        }

        public Stream GetAssetOrResourceFile(string fileName)
        {
            return Android.App.Application.Context.Assets.Open(fileName);
        }

        public async Task WriteNewFileFromStream(Stream fileStream, string path)
        {
            // create a write stream
            FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            // write to the stream
            await ReadWriteStream(fileStream, writeStream);
        }

        private async Task ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = await readStream.ReadAsync(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                await writeStream.WriteAsync(buffer, 0, bytesRead);
                bytesRead = await readStream.ReadAsync(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}