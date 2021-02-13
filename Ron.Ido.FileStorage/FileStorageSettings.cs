using Ron.Ido.Common.Extensions;

namespace Ron.Ido.FileStorage
{
    [SectionName("FileStorage")]
    public class FileStorageSettings
    {
        public string TempDir { get; set; }

        public string FilesRoot { get; set; }
    }
}
