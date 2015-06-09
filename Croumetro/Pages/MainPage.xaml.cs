using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Croumetro.Common;
using Croumetro.Tools.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Croumetro.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var loginTest = await Locator.ViewModels.MainPageVm.LoginTest();
            if (!loginTest)
            {
                Locator.ViewModels.MainPageVm.ToLoginPage.Execute(null);
                return;
            }

            App.RootFrame = MainFrame;
            App.RootFrame.Navigated += RootFrameOnNavigated;

            var launchString = (string)e.NavigationParameter;
            if (!string.IsNullOrEmpty(launchString))
            {
                
            }
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = (Splitter.IsPaneOpen != true);
        }

        private async void Picture_OnClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");
            var file = await openPicker.PickSingleFileAsync();
            if (file == null) return;
            if (!file.ContentType.Contains("image"))
            {
                return;
            }
            Locator.ViewModels.MainPageVm.ImageFile = file;
            var stream = await file.OpenAsync(FileAccessMode.Read);
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(stream);
            SelectedImage.Source = bitmap;
        }

        private void RootFrameOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = App.RootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MenuClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            menuItem?.Command.Execute(null);
            if (Splitter.IsPaneOpen)
            {
                Splitter.IsPaneOpen = false;
            }
        }
    }
}
