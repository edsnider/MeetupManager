using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsStore.Platform;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Win8.PlatformSpecific;
using Refractored.MvxPlugins.Settings;
using Refractored.MvxPlugins.Settings.WindowsStore;
using Windows.UI.Xaml.Controls;

namespace MeetupManager.Win8
{
    public class Setup : MvxStoreSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Portable.App();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<IMessageDialog>(() => new Win8MessageDialog());
            Mvx.RegisterSingleton<ILogin>(() => new Win8MeetupLogin());
            Mvx.RegisterSingleton<ISettings>(() => new MvxWindowsStoreSettings());
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}