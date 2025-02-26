using Microsoft.Win32;

namespace NotesApp.Services
{
    public class DialogService : IDialogService
    {
        public string? ShowOpenFileDialog(string filter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string? ShowSaveFileDialog(string filter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}