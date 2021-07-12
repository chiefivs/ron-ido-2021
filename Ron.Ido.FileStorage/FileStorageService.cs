using FluentFTP;
using Ron.Ido.Common.Extensions;
using Ron.Ido.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Ron.Ido.FileStorage
{
    public class FileStorageService: IFileStorageService
    {
        private readonly FileStorageSettings _settings;
        private readonly bool _isFtp;
        private static readonly object _ftpParamsLock = new object();
        private static FtpParams _ftpParams;

        public FileStorageService(FileStorageSettings settings)
        {
            _settings = settings;
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

        public Guid CreateFile(byte[] data)
        {
            var uid = Guid.NewGuid();

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

            return uid;
        }

        public Guid CreateTempFile(byte[] data)
        {
            var uid = Guid.NewGuid();
            string temppath = Path.Combine(TempDir, uid.ToString());
            File.WriteAllBytes(temppath, data);

            return uid;
        }

        public void DeleteTempFile(Guid uid)
        {
            string temppath = Path.Combine(TempDir, uid.ToString());
            if (!File.Exists(temppath))
                return;

            File.Delete(temppath);
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

        private void _createDirRecursive(string path)
        {
            if (Directory.Exists(path))
                return;

            var parent = Path.GetDirectoryName(path);
            _createDirRecursive(parent);
            Directory.CreateDirectory(path);
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
