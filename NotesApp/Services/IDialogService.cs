using Microsoft.Win32;

namespace NotesApp.Services
{
    public interface IDialogService
    {
        string? ShowOpenFileDialog(string filter);
        string? ShowSaveFileDialog(string filter);
    }
}