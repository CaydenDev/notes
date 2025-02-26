using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class NoteStorageService
    {
        private readonly string _filePath;

        public NoteStorageService()
        {
            var directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "NotesApp"
            );
            
            _filePath = Path.Combine(directory, "notes.json");

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public async Task SaveNotesAsync(IEnumerable<Note> notes, string? customPath = null)
        {
            var path = customPath ?? _filePath;
            var json = JsonSerializer.Serialize(notes, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            await File.WriteAllTextAsync(path, json);
        }

        public async Task<IEnumerable<Note>> LoadNotesAsync(string? customPath = null)
        {
            var path = customPath ?? _filePath;
            if (!File.Exists(path))
            {
                return new List<Note>();
            }

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<IEnumerable<Note>>(json) ?? new List<Note>();
        }
    }
}