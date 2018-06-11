using System;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ConverterUtilities {
    public class Toast {

        public static Toast Instance = new Toast();
        private void Main() { }

        private Notifier notifier;

        public void CreateNotifier() {
            notifier = new Notifier(cfg => {
                cfg.PositionProvider = new WindowPositionProvider(parentWindow: CUtilities.MainWindow, corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
                cfg.Dispatcher = CUtilities.Dispatcher;
            });
        }

        #region Messages
        public void NoUpdate() => notifier.ShowSuccess("There is no update!"); // Shows if there is no update
        public void Update(string currentVersion, string updatedVersion) => notifier.ShowInformation($"There is an update. Your version: {currentVersion} Updated version: {updatedVersion}"); //This message shows when there is an update
        public void UpdateCheckFail() => notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        public void BetaRelease() => notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        public void ConvertFinished() => notifier.ShowInformation("Finished Converting"); //Finished message
        public void AlreadyConverting() => notifier.ShowWarning("Already Converting"); //Already converting message
        public void SettingsSaved() => notifier.ShowSuccess("Settings were saved!"); //Settings saved message
        public void SettingsReset() => notifier.ShowInformation("Settings reset, make sure you save them!"); //Reset message and a reminder to save them
        public void VideoMessage() => notifier.ShowWarning("Video conversion can take a long time to finish. Currently I do not show progress. It is not recommended that you convert multiple videos at once."); //Message for video
        #endregion

        public void Dispose() => notifier.Dispose();
    }
}
