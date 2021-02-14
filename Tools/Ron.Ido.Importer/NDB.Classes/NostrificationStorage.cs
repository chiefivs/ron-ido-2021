using System.Collections.Generic;
using System.IO;

namespace Ron.Ido.Importer.NDB.Classes
{
    public class NostrificationStorage
    {
        private string _storagePath;

        public NostrificationStorage(string storagePath)
        {
            _storagePath = storagePath;
        }

        public byte[] GetFileBytes(UploadedFile file)
        {
            if (file.Data != null)
                return file.Data;

            var pathParts = new Stack<string>(file.FileName.Split('\\'));
            if (pathParts.Count < 2)
                return null;

            var fileName = pathParts.Pop();
            var fileDir = pathParts.Pop();
            var filePath = Path.Combine(_storagePath, fileDir, fileName);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllBytes(filePath);
        }
    }

}
