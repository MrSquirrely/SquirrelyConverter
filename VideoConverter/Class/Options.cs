namespace VideoConverter.Class {
    internal class Options {
        internal static bool CreateTemp {
            get => Properties.Settings.Default.Create_Temp;
            set => Properties.Settings.Default.Create_Temp = value;
        }

        internal static string TempLocation {
            get => Properties.Settings.Default.Temp_Location;
            set => Properties.Settings.Default.Temp_Location = value;
        }

        internal static bool ChangeSize {
            get => Properties.Settings.Default.Change_Size;
            set => Properties.Settings.Default.Change_Size = value;
        }

        internal static int Width {
            get => Properties.Settings.Default.Width;
            set => Properties.Settings.Default.Width = value;
        }

        internal static int Height {
            get => Properties.Settings.Default.Height;
            set => Properties.Settings.Default.Height = value;
        }

        internal static bool RemoveAudio {
            get => Properties.Settings.Default.Remove_Audio;
            set => Properties.Settings.Default.Remove_Audio = value;
        }

        internal static bool DeleteFile {
            get => Properties.Settings.Default.Delete_File;
            set => Properties.Settings.Default.Delete_File = value;
        }

        internal static void Save() => Properties.Settings.Default.Save();
    }
}
