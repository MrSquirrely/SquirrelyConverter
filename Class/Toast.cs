using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Mr_Squirrely_Converters.Class
{
    class Toast {
        private static Notifier notifier;

        internal static void CreateNotifier() =>
            notifier = new Notifier(cfg => {
                 cfg.PositionProvider = new PrimaryScreenPositionProvider(corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
                 cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
                 cfg.Dispatcher = App.Current.Dispatcher;
             });
        #region Messages
        internal static void NoUpdate() => notifier.ShowSuccess("There is no update!");
        internal static void Update() => notifier.ShowInformation($"There is an update. Your version: {Utils._CurrentVersion} Updated version: {Utils._UpdateVerstion}"); //This message shows when there is an update
        internal static void UpdateCheckFail() => notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        internal static void BetaRelease() => notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        internal static void ConvertFinished() => notifier.ShowInformation("Finished Converting"); //Finished message
        internal static void AlreadyConverting() => notifier.ShowWarning("Already Converting"); //Already converting message
        #endregion

        internal static void Dispose() => notifier.Dispose(); //Dispose when we are done
    }
}
