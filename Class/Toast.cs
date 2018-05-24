using System;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Mr_Squirrely_Converters.Class;

namespace Mr_Squirrely_Converters.Class {
    class Toast {
        private static Notifier Notifier;

        internal static void CreateNotifier() {
            Notifier = new Notifier(cfg => {
                cfg.PositionProvider = new WindowPositionProvider(parentWindow: Utilities.MainWindow, corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
                cfg.Dispatcher = App.Current.Dispatcher;
            });
        }

        #region Messages
        internal static void NoUpdate() => Notifier.ShowSuccess("There is no update!"); // Shows if there is no update
        internal static void Update() => Notifier.ShowInformation($"There is an update. Your version: {Utilities.CurrentVersion} Updated version: {Utilities.UpdateVerstion}"); //This message shows when there is an update
        internal static void UpdateCheckFail() => Notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        internal static void BetaRelease() => Notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        internal static void ConvertFinished() => Notifier.ShowInformation("Finished Converting"); //Finished message
        internal static void AlreadyConverting() => Notifier.ShowWarning("Already Converting"); //Already converting message
        internal static void SettingsSaved() => Notifier.ShowSuccess("Settings were saved!"); //Settings saved message
        internal static void SettingsReset() => Notifier.ShowInformation("Settings reset, make sure you save them!"); //Reset message and a reminder to save them
        internal static void VideoMessage() => Notifier.ShowWarning("Video conversion can take a long time to finish. Currently I do not show progress. It is not recommended that you convert multiple videos at once."); //Message for video
        #endregion

        internal static void Dispose() => Notifier.Dispose(); //Dispose when we are done
    }
}
