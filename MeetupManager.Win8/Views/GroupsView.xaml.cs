using Cirrious.MvvmCross.WindowsStore.Views;
using MeetupManager.Portable.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MeetupManager.Win8.Views
{
    /// <summary>
    /// My Groups view
    /// </summary>
    public sealed partial class GroupsView : MvxStorePage
    {
        public new GroupsViewModel ViewModel
        {
            get { return (GroupsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public GroupsView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel.RefreshCommand.Execute();
        }
    }
}
