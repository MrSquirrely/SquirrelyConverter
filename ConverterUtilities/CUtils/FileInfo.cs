using System.IO;

namespace ConverterUtilities.CUtils {
    /// <summary>
    /// Used to get file information.
    /// </summary>
    class FileInfo {
        protected string File;
        public FileInfo(string file) => File = file;
        public string FileName() => Path.GetFileName(File);
        public string FileNameWithoutExtension() => Path.GetFileNameWithoutExtension(File);
        public string FileType() => Path.GetExtension(File);
        public string FileDirectory() => Path.GetDirectoryName(File);
        public string FileLocation() => Path.GetFullPath(File);
    }
}
