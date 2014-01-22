using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using BooksCollector.Client.ShellHelpers;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;
using IPersistFile = System.Runtime.InteropServices.ComTypes.IPersistFile;

namespace BooksCollector.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>3
    public partial class App : Application
    {
        public static readonly String AppID = "BooksCollector.Client";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TryCreateShortcut();
        }
        // In order to display toasts, a desktop application must have a shortcut on the Start menu. 
        // Also, an AppUserModelID must be set on that shortcut. 
        // The shortcut should be created as part of the installer. The following code shows how to create 
        // a shortcut and assign an AppUserModelID using Windows APIs. You must download and include the  
        // Windows API Code Pack for Microsoft .NET Framework for this code to function 
        // 
        // Included in this project is a wxs file that be used with the WiX toolkit 
        // to make an installer that creates the necessary shortcut. One or the other should be used. 
        private bool TryCreateShortcut()
        {
            var shortcutPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Microsoft\Windows\Start Menu\Programs\Books Collector.lnk");
            if (!File.Exists(shortcutPath))
            {
                InstallShortcut(shortcutPath);
                return true;
            }
            return false;
        }

        private void InstallShortcut(String shortcutPath)
        {
            // Find the path to the current executable 
            var exePath = Process.GetCurrentProcess().MainModule.FileName;
            var newShortcut = (IShellLinkW)new CShellLink();

            // Create a shortcut to the exe 
            ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            // Open the shortcut property store, set the AppUserModelId property 
            var newShortcutProperties = (IPropertyStore)newShortcut;

            using (var appId = new PropVariant(AppID))
            {
                ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
                ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            }

            // Commit the shortcut to disk 
            var newShortcutSave = (IPersistFile)newShortcut;
            newShortcutSave.Save(shortcutPath, true);
         //   ErrorHelper.VerifySucceeded();
        } 
    }
}
