using System.IO;
using System.Threading.Tasks;

namespace XamarinBoilerplate.Interfaces
{
    public interface IFileManager
    {
        string PersonalFolderPath { get; }
        Stream GetAssetOrResourceFile(string fileName);
        Task WriteNewFileFromStream(Stream fileStream, string path);
    }
}
