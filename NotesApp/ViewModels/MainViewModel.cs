using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;  // Add this line
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesApp.Models;
using NotesApp.Services;

namespace NotesApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly NoteStorageService _storageService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Note> Notes { get; } = new();

        [ObservableProperty]
        private Note? selectedNote;

        [ObservableProperty]
        private int selectedFontSize = 14;

        public MainViewModel(NoteStorageService storageService, IDialogService dialogService)
        {
            _storageService = storageService;
            _dialogService = dialogService;
            LoadNotes();
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedNote != null)
            {
                SelectedNote.ModifiedAt = DateTime.Now;
                await _storageService.SaveNotesAsync(Notes);
            }
        }

        [RelayCommand]
        private async Task SaveAs()
        {
            var path = _dialogService.ShowSaveFileDialog("JSON Files (*.json)|*.json");
            if (!string.IsNullOrEmpty(path))
            {
                await _storageService.SaveNotesAsync(Notes, path);
            }
        }

        [RelayCommand]
        private async Task Open()
        {
            var path = _dialogService.ShowOpenFileDialog("JSON Files (*.json)|*.json");
            if (!string.IsNullOrEmpty(path))
            {
                var notes = await _storageService.LoadNotesAsync(path);
                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        [RelayCommand]
        private void FormatBold()
        {
            // Implementation will be in code-behind
        }

        [RelayCommand]
        private void FormatItalic()
        {
            // Implementation will be in code-behind
        }

        [RelayCommand]
        private void FormatUnderline()
        {
            // Implementation will be in code-behind
        }

        partial void OnSelectedFontSizeChanged(int value)
        {
            // Implementation will be in code-behind
        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private async void LoadNotes()
        {
            var notes = await _storageService.LoadNotesAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        [RelayCommand]
        private void CreateNote()
        {
            var note = new Note();
            Notes.Add(note);
            SelectedNote = note;
            SaveNotes();
        }

        [RelayCommand]
        private void DeleteNote(Note note)
        {
            if (SelectedNote == note)
                SelectedNote = null;

            Notes.Remove(note);
            SaveNotes();
        }

        [RelayCommand]
        private void UpdateNote(Note note)
        {
            note.ModifiedAt = DateTime.Now;
            SaveNotes();
        }

        private async void SaveNotes()
        {
            await _storageService.SaveNotesAsync(Notes);
        }
        [RelayCommand]
        private async Task Reload()
        {
            var notes = await _storageService.LoadNotesAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}