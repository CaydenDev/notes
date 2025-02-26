using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NotesApp.Models
{
    public partial class Note : ObservableObject
    {
        [ObservableProperty]
        private string id = Guid.NewGuid().ToString();

        [ObservableProperty]
        private string title = "New Note";

        [ObservableProperty]
        private string content = string.Empty;

        [ObservableProperty]
        private string richContent = string.Empty;

        [ObservableProperty]
        private DateTime createdAt = DateTime.Now;

        [ObservableProperty]
        private DateTime modifiedAt = DateTime.Now;
    }
}