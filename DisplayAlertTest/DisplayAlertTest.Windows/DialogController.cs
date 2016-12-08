using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DisplayAlertTest.Windows;
using Xamarin.Forms;

[assembly:Dependency(typeof(DialogController))]

namespace DisplayAlertTest.Windows
{
    public class DialogController : IDialogController
    {
        public Task DisplayAlert(string title, string message, string button)
        {
            var tcs = new TaskCompletionSource<bool>();
            var messageDialog = new MessageDialog(message, title);
            messageDialog.Commands.Add(new UICommand(button, command => tcs.SetResult(true)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.ShowAsync().AsTask();
            return tcs.Task;
        }
    }
}