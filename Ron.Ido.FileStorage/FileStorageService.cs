using FluentFTP;
using Microsoft.Extensions.Configuration;
using Ron.Ido.Common.Extensions;
using Ron.Ido.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ron.Ido.FileStorage
{
    public interface IFileStorageService<TFileInfo> : IFileStorageService where TFileInfo : class, IFileInfo, new()
    {
        new string GetTempFilePath(string filename);
        new TFileInfo CreateFile(byte[] data, string filename, string contentType);
        new TFileInfo CreateTempFile(byte[] data, string filename, string contentType);
        new TFileInfo SaveFile(Guid uid);
        new Guid SaveFile(byte[] bytes);
        new void DeleteFile(Guid uid);
        new byte[] GetFileBytes(Guid uid);
        new TFileInfo GetFileInfo(Guid uid);
    }

    public class FileStorageService<TFileInfo> : IFileStorageService<TFileInfo> where TFileInfo : class, IFileInfo, new()
    {
        private readonly FileStorageSettings _settings;
        private readonly bool _isFtp;
        private static readonly object _ftpParamsLock = new object();
        private static FtpParams _ftpParams;

        public FileStorageService(IConfiguration config)
        {
            _settings = config.GetSettings<FileStorageSettings>();
            _isFtp = _settings.FilesRoot.StartsWith("ftp://");
        }

        private string TempDir
        {
            get
            {
                _createDirRecursive(_settings.TempDir);
                return _settings.TempDir;
            }
        }

        private string FilesDir
        {
            get
            {
                _createDirRecursive(_settings.FilesRoot);
                return _settings.FilesRoot;
            }
        }

        private FtpParams FtpSettings
        {
            get
            {
                if (_ftpParams == null)
                {
                    lock (_ftpParamsLock)
                    {
                        if (_ftpParams == null)
                        {
                            _ftpParams = new FtpParams(_settings.FilesRoot);
                        }
                    }
                }

                return _ftpParams;
            }
        }

        public string GetTempFilePath(string filename)
        {
            return Path.Combine(TempDir, filename);
        }

        public IFileInfo CreateFile(byte[] data, string filename, string contentType)
        {
            return _createFile(data, filename, contentType);
        }

        TFileInfo IFileStorageService<TFileInfo>.CreateFile(byte[] data, string filename, string contentType)
        {
            return _createFile(data, filename, contentType);
        }

        private TFileInfo _createFile(byte[] data, string filename, string contentType)
        {
            var uid = Guid.NewGuid();

            var attachment = new TFileInfo
            {
                Uid = uid,
                Name = filename,
                Size = data.Length,
                ContentType = contentType
            };

            if (_isFtp)
            {
                var pars = FtpSettings;
                using (var client = _createFtpClient())
                {
                    string filepath = Path.Combine(FtpSettings.Path, uid.ToString());
                    client.Upload(data, filepath);
                }
            }
            else
            {
                string path = Path.Combine(FilesDir, uid.ToString());
                File.WriteAllBytes(path, data);
            }

            return attachment;
        }

        public TFileInfo CreateTempFile(byte[] data, string filename, string contentType)
        {
            return _createTempFile(data, filename, contentType);
        }

        IFileInfo IFileStorageService.CreateTempFile(byte[] data, string filename, string contentType)
        {
            return _createTempFile(data, filename, contentType);
        }

        private TFileInfo _createTempFile(byte[] data, string filename, string contentType)
        {
            var uid = Guid.NewGuid();

            var attachment = new TFileInfo
            {
                Uid = uid,
                Name = filename,
                Size = data.Length,
                ContentType = contentType
            };

            string temppath = Path.Combine(TempDir, uid.ToString());
            string metapath = temppath + ".meta";

            File.WriteAllBytes(temppath, data);

            using (var stream = File.Create(metapath))
            {
                var formatter = new BinaryFormatter();
                #pragma warning disable SYSLIB0011 // Type or member is obsolete
                formatter.Serialize(stream, new FileMeta<TFileInfo>(attachment));
                #pragma warning restore SYSLIB0011 // Type or member is obsolete
            }

            return attachment;
        }

        public TFileInfo SaveFile(Guid uid)
        {
            return _saveFile(uid);
        }

        IFileInfo IFileStorageService.SaveFile(Guid uid)
        {
            return _saveFile(uid);
        }

        private TFileInfo _saveFile(Guid uid)
        {
            string temppath = Path.Combine(TempDir, uid.ToString());
            if (!File.Exists(temppath))
                return null;

            if (_isFtp)
            {
                using (var client = _createFtpClient())
                {
                    string remotepath = Path.Combine(FtpSettings.Path, uid.ToString());
                    client.UploadFile(temppath, remotepath);
                    File.Delete(temppath);
                }
            }
            else
            {
                string filepath = Path.Combine(FilesDir, uid.ToString());
                File.Move(temppath, filepath);
            }

            string metapath = temppath + ".meta";
            var attachment = _deserializeFileInfo(metapath);
            if (File.Exists(metapath))
                File.Delete(metapath);

            return attachment;
        }

        public Guid SaveFile(byte[] bytes)
        {
            var uid = Guid.NewGuid();

            if (_isFtp)
            {
                using (var client = _createFtpClient())
                {
                    string filepath = Path.Combine(FtpSettings.Path, uid.ToString());
                    client.Upload(bytes, filepath);
                }
            }
            else
            {
                string filepath = Path.Combine(FilesDir, uid.ToString());
                File.WriteAllBytes(filepath, bytes);
            }

            return uid;
        }

        public void DeleteFile(Guid uid)
        {
            if (_isFtp)
            {
                using (var client = _createFtpClient())
                {
                    string filepath = Path.Combine(FtpSettings.Path, uid.ToString());
                    client.DeleteFile(filepath);
                }
            }
            else
            {
                string filepath = Path.Combine(FilesDir, uid.ToString());
                if (!File.Exists(filepath))
                    return;

                File.Delete(filepath);
            }
        }

        public byte[] GetFileBytes(Guid uid)
        {
            byte[] result = null;
            if (_isFtp)
            {
                using (var client = _createFtpClient())
                {
                    string filepath = Path.Combine(FtpSettings.Path, uid.ToString());
                    if (client.FileExists(filepath))
                    {
                        using (var stream = new MemoryStream())
                        {
                            if (client.Download(stream, filepath))
                                result = stream.ToArray();
                        }
                    }
                }
            }
            else
            {
                string path = Path.Combine(FilesDir, uid.ToString());
                if (File.Exists(path))
                    result = File.ReadAllBytes(path);
            }

            if (result == null)
            {
                string temppath = Path.Combine(TempDir, uid.ToString());
                if (File.Exists(temppath))
                    result = File.ReadAllBytes(temppath);
            }

            return result;
        }

        public TFileInfo GetFileInfo(Guid uid)
        {
            return _getFileInfo(uid);
        }

        IFileInfo IFileStorageService.GetFileInfo(Guid uid)
        {
            return _getFileInfo(uid);
        }

        private TFileInfo _getFileInfo(Guid uid)
        {
            string metapath = Path.Combine(TempDir, uid.ToString()) + ".meta";
            if (!File.Exists(metapath))
            {
                return null;
            }

            return _deserializeFileInfo(metapath);
        }

        private void _createDirRecursive(string path)
        {
            if (Directory.Exists(path))
                return;

            var parent = Path.GetDirectoryName(path);
            _createDirRecursive(parent);
            Directory.CreateDirectory(path);
        }

        private static TFileInfo _deserializeFileInfo(string temppath)
        {
            using (var stream = File.OpenRead(temppath))
            {
                var formatter = new BinaryFormatter();
                #pragma warning disable SYSLIB0011 // Type or member is obsolete
                var meta = formatter.Deserialize(stream) as FileMeta<TFileInfo>;
                #pragma warning restore SYSLIB0011 // Type or member is obsolete
                return meta.GetFileInfo();
            }
        }

        private FtpClient _createFtpClient()
        {
            var pars = FtpSettings;
            return new FtpClient(pars.Host, pars.Port, pars.Login, pars.Password);
        }

        private class FtpParams
        {
            public string Host;
            public int Port;
            public string Path;
            public string Login;
            public string Password;

            public FtpParams(string setting)
            {
                setting = setting.Replace("ftp://", "");
                var credAddrParts = setting.Split("@");

                var loginPassParts = credAddrParts[0].Split(":");
                Login = loginPassParts[0];
                Password = loginPassParts[1];

                var hostPortParts = credAddrParts[1].Split(":");
                Host = hostPortParts[0];

                var portPathParts = new Queue<string>(hostPortParts[1].Split("/"));
                Port = portPathParts.Dequeue().Parse(0);

                Path = portPathParts.Join("/");
            }
        }
    }
}
