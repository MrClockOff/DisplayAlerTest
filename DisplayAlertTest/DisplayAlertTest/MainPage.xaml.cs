using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisplayAlertTest
{
    public partial class MainPage : ContentPage
    {
        private const string DialoTitle = "Test Title";
        private const string DialogMessage = "Test Message";
        private const string DialogButton = "OK";

        public MainPage()
        {
            InitializeComponent();
        }

        private void DisplayXamarinAlert_OnClicked(object sender, EventArgs e)
        {
            InvokeFromBackgroundThread(() => Application.Current.MainPage.DisplayAlert(DialoTitle, DialogMessage, DialogButton));
        }

        private void DisplayDependencyAlert_OnClicked(object sender, EventArgs e)
        {
            InvokeFromBackgroundThread(() => DependencyService.Get<IDialogController>().DisplayAlert(DialoTitle, DialogMessage, DialogButton));
        }

        private static async void InvokeFromBackgroundThread(Func<Task> action)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(
                    async () =>
                    {
                        try
                        {
                            await action.Invoke().ContinueWith(
                                t =>
                                {
                                    if (t.IsFaulted)
                                    {
                                        Debug.WriteLine("I CAN HANDLE THAT! 2");
                                    }
                                });
                        }
                        catch
                        {
                            Debug.WriteLine("I CAN HANDLE THAT! 1");
                        }
                    });
            });
        }
    }
}
