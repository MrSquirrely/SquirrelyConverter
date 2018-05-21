using System;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Mr_Squirrely_Converters.Class {
    class Toast {
        private static Notifier _Notifier;

        internal static void CreateNotifier() {
            _Notifier = new Notifier(cfg => {
            cfg.PositionProvider = new WindowPositionProvider(parentWindow: Utils._MainWindow, corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
            cfg.Dispatcher = App.Current.Dispatcher;
            });
        }
        #region Messages
        internal static void NoUpdate() => _Notifier.ShowSuccess("There is no update!");// Shows if there is no update
        internal static void Update() => _Notifier.ShowInformation($"There is an update. Your version: {Utils._CurrentVersion} Updated version: {Utils._UpdateVerstion}"); //This message shows when there is an update
        internal static void UpdateCheckFail() => _Notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        internal static void BetaRelease() => _Notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        internal static void ConvertFinished() => _Notifier.ShowInformation("Finished Converting"); //Finished message
        internal static void AlreadyConverting() => _Notifier.ShowWarning("Already Converting"); //Already converting message
        internal static void SettingsSaved() => _Notifier.ShowSuccess("Settings were saved!"); //Settings saved message
        internal static void SettingsReset() => _Notifier.ShowInformation("Settings reset, make sure you save them!"); //Reset message and a reminder to save them
        internal static void VideoMessage() => _Notifier.ShowWarning("Video conversion can take a long time to finish. Currently I do not show progress. It is not recommended that you convert multiple videos at once.");//Message for video
        internal static void VideoMessage2() => _Notifier.ShowWarning("Currently converting of videos is disabled.");
        #endregion

        internal static void Dispose() => _Notifier.Dispose(); //Dispose when we are done
    }
}
