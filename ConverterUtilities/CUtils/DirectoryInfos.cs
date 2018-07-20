namespace ConverterUtilities.CUtils {
    public class DirectoryInfos {
        public static string WorkingDirectory { get; set; }
        //Todo: add GetTempDir
        public static string GetTempDir(bool createTemp, string tempLocation) => $"{WorkingDirectory}\\{(createTemp ? tempLocation : "converter_temp")}";
    }
}
