using Foundation;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBoilerplate.Interfaces;
using XamarinBoilerplate.iOS.DependencyServices;

[assembly: Dependency(typeof(FileManagerDependencyService))]
namespace XamarinBoilerplate.iOS.DependencyServices
{
    public class FileManagerDependencyService : IFileManager
    {
        public string PersonalFolderPath
        {
            get
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(documentsPath, "..", "Library");
            }
        }

        public Stream GetAssetOrResourceFile(string fileName)
        {
            string[] file = fileName.Split('.');
            string path = NSBundle.MainBundle.PathForResource(file[0], file[1]);
            //var filePath = Path.Combine(path, fileName);
            //var someFileContents = File.ReadAllBytes(filePath);
            FileStream fileStream = new FileStream(path: path, mode: FileMode.Open, access: FileAccess.Read);

            return fileStream;
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