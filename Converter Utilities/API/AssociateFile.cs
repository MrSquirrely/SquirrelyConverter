using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Converter_Utilities.API {
    public class AssociateFile {

        public static void SetAssociation(string applicaitonFilePath) => Associate(".cext", "CONVERTER_EXTENSION_FILE", applicaitonFilePath, "Conv");

        private static void Associate(string extension, string keyName, string openWith, string fileDescription) {
            try {
                RegistryKey BaseKey;
                RegistryKey OpenMethod;
                RegistryKey Shell;
                RegistryKey CurrentUser;

                BaseKey = Registry.ClassesRoot.CreateSubKey(extension);
                BaseKey?.SetValue("", keyName);

                OpenMethod = Registry.ClassesRoot.CreateSubKey(keyName);
                OpenMethod?.SetValue("", fileDescription);
                OpenMethod?.CreateSubKey("DefaultIcon")?.SetValue("", $"\"{openWith}\"");

                Shell = OpenMethod?.CreateSubKey("Shell");
                Shell?.CreateSubKey("edit")?.CreateSubKey("command")?.SetValue("",$"\"{openWith}\"");
                Shell?.CreateSubKey("open")?.CreateSubKey("command")?.SetValue("",$"\"{openWith}\"");

                BaseKey?.Close();
                OpenMethod?.Close();
                Shell?.Close();

                CurrentUser = Registry.CurrentUser.OpenSubKey($"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\{extension}", true);
                CurrentUser?.DeleteSubKey("UserChoice", false);
                CurrentUser?.Close();

                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception ex) {
                Logger.Instance("AssociateFile").LogError(ex);
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    }
}
