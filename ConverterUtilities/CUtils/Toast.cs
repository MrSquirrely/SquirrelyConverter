using System;
using ConverterUtilities.Configs;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ConverterUtilities.CUtils {
    public static class Toast {
        private static Notifier _notifier;

        public static void CreateNotifier() {
            _notifier = new Notifier(cfg => {
                cfg.PositionProvider = new WindowPositionProvider(parentWindow:CUtilities.MainWindow, corner:Corner.BottomRight, offsetX:10, offsetY:10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
                cfg.Dispatcher = CUtilities.Dispatcher;
            });
        }

        #region Messages

        public static void CustomMessage(Enums.MessageType messageType, string message) {
            switch (messageType) {
                case Enums.MessageType.Error:
                    _notifier.ShowError(message);
                    break;
                case Enums.MessageType.Info:
                    _notifier.ShowInformation(message);
                    break;
                case Enums.MessageType.Success:
                    _notifier.ShowSuccess(message);
                    break;
                case Enums.MessageType.Warning:
                    _notifier.ShowWarning(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null);
            }
        }

        public static void NoUpdate() {
            try {
                _notifier.ShowSuccess("There is no update!"); // Shows if there is no update
            }
            catch (Exception ex) {
                Logger.LogDebug(ex);
            }
        }

        public static void NothingToConvert() => _notifier.ShowWarning("There isn't anything to convert.");
        public static void Update(string type, double currentVersion, double updatedVersion) => _notifier.ShowInformation($"There is an update for {type}. \nYour version is: {currentVersion} \nThe updated version is: {updatedVersion}"); //This message shows when there is an update
        public static void UpdateCheckFail() => _notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        public static void PreviewRelease() => _notifier.ShowInformation("This is a preview release, some things might not work. Please use this with caution and report any bugs."); //Beta release notice
        public static void ConvertFinished() => _notifier.ShowInformation("Finished Converting"); //Finished message
        public static void AlreadyConverting() => _notifier.ShowWarning("Already Converting"); //Already converting message
        public static void SettingsSaved() => _notifier.ShowSuccess("Settings were saved!"); //Settings saved message
        public static void SettingsReset() => _notifier.ShowInformation("Settings reset, make sure you save them!"); //Reset message and a reminder to save them
        public static void VideoMessage() => _notifier.ShowWarning("Video conversion can take a long time to finish. Currently progress isn't shown. It is not recommended that you convert multiple videos at once."); //Message for video and a warning not to convert multiple files
        #endregion

        public static void Dispose() => _notifier.Dispose();

    }
}
