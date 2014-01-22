using System.Diagnostics;
using Windows.UI.Notifications;

namespace BooksCollector.Client
{
    public class Toaster
    {
        // Creates and shows the toast.
        public static void ShowToast(string header, string message)
        {
            // Get a toast XML template
            var toastXml = ToastNotificationManager
                .GetTemplateContent(ToastTemplateType.ToastText02);

            // Fill in the text elements
            var headerElement = toastXml.FirstChild.FirstChild.FirstChild.FirstChild; //.GetElementById("1");
            headerElement.AppendChild(toastXml.CreateTextNode(header));

            var textElement = toastXml.FirstChild.FirstChild.FirstChild.LastChild; //.GetElementById("2");
            textElement.AppendChild(toastXml.CreateTextNode(message));

            // Create the toast and attach event listeners
            var toast = new ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;

            // Show the toast. Be sure to specify the AppId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(App.AppID).Show(toast);
        }

        private static void ToastActivated(ToastNotification sender, object e)
        {
            Trace.WriteLine("The user activated the toast.");
        }

        private static void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            var outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }

            Trace.WriteLine(outputText);
        }

        private static void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            Trace.WriteLine("The toast encountered an error.");
        }
    }
}