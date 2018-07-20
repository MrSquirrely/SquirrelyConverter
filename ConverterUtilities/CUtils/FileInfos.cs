using System.IO;

namespace ConverterUtilities.CUtils {
    /// <summary>
    /// Used to get file information.
    /// </summary>
    public class FileInfos {
        protected string File;
        public FileInfos(string file) => File = file;
        /// <summary>
        /// Get the file name with the extension
        /// </summary>
        /// <returns></returns>
        public string FileName() => Path.GetFileName(File);
        /// <summary>
        /// Get the file name without the extension
        /// </summary>
        /// <returns></returns>
        public string FileNameWithoutExtension() => Path.GetFileNameWithoutExtension(File);
        /// <summary>
        /// Get the file extension
        /// </summary>
        /// <returns></returns>
        public string FileType() => Path.GetExtension(File);
        /// <summary>
        /// Get the directory of the file
        /// </summary>
        /// <returns></returns>
        public string FileDirectory() => Path.GetDirectoryName(File);
        /// <summary>
        /// Get the full path of the file
        /// </summary>
        /// <returns></returns>
        public string FileLocation() => Path.GetFullPath(File);
    }
}
