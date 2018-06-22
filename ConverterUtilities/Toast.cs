using System;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ConverterUtilities {
    public static class Toast {
        private static Notifier _notifier;

        public static void CreateNotifier() {
            _notifier = new Notifier(cfg => {
                cfg.PositionProvider = new PrimaryScreenPositionProvider( corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
                cfg.Dispatcher = CUtilities.Dispatcher;
            });
        }

        #region Messages
        public static void NoUpdate() {
            try {
                _notifier.ShowSuccess("There is no update!"); // Shows if there is no update

            }
            catch (Exception ex) {
                Logger.LogDebug(ex);
            }
        }

        public static void Update(string currentVersion, string updatedVersion) => _notifier.ShowInformation($"There is an update. Your version: {currentVersion} Updated version: {updatedVersion}"); //This message shows when there is an update
        public static void UpdateCheckFail() => _notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        public static void BetaRelease() => _notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        public static void ConvertFinished() => _notifier.ShowInformation("Finished Converting"); //Finished message
        public static void AlreadyConverting() => _notifier.ShowWarning("Already Converting"); //Already converting message
        public static void SettingsSaved() => _notifier.ShowSuccess("Settings were saved!"); //Settings saved message
        public static void SettingsReset() => _notifier.ShowInformation("Settings reset, make sure you save them!"); //Reset message and a reminder to save them
        public static void VideoMessage() => _notifier.ShowWarning("Video conversion can take a long time to finish. Currently I do not show progress. It is not recommended that you convert multiple videos at once."); //Message for video
        #endregion

        public static void Dispose() => _notifier.Dispose();
    }
}
