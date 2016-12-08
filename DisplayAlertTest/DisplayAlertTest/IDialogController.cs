using System.Threading.Tasks;

namespace DisplayAlertTest
{
    public interface IDialogController
    {
        Task DisplayAlert(string title, string message, string button);
    }
}