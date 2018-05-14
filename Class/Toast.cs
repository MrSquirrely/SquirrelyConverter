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
        private Notifier notifier = new Notifier(cfg =>{
            cfg.PositionProvider = new PrimaryScreenPositionProvider(corner: Corner.BottomRight, offsetX: 10, offsetY: 10);
            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(notificationLifetime: TimeSpan.FromSeconds(5), maximumNotificationCount: MaximumNotificationCount.FromCount(5));
            cfg.Dispatcher = App.Current.Dispatcher;
            });

        #region Messages
        public void NoUpdate() => notifier.ShowSuccess("There is no update!");
        public void Update() => notifier.ShowInformation($"There is an update. Your version: {Utils._CurrentVersion} Updated version: {Utils._UpdateVerstion}"); //This message shows when there is an update
        public void UpdateCheckFail() => notifier.ShowWarning("Failed to check for update. Please try again."); //In case checking for the update fails
        public void BetaRelease() => notifier.ShowInformation("This is a beta release so some things are not finished."); //Beta release notice
        public void ConvertFinished() => notifier.ShowInformation("Finished Converting"); //Finished message
        public void AlreadyConverting() => notifier.ShowWarning("Already Converting"); //Already converting message
        #endregion

        public void Dispose() => notifier.Dispose(); //Dispose when we are done
    }
}
