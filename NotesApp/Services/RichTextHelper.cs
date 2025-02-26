using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NotesApp.Services
{
    public static class RichTextHelper
    {
        public static string GetRichTextXaml(RichTextBox richTextBox)
        {
            if (richTextBox == null) return string.Empty;
            
            TextRange range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Xaml);
                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static void SetRichTextXaml(RichTextBox richTextBox, string xamlString)
        {
            if (richTextBox == null) return;
            
            if (string.IsNullOrEmpty(xamlString))
            {
                richTextBox.Document.Blocks.Clear();
                return;
            }

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xamlString)))
            {
                TextRange range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                range.Load(stream, DataFormats.Xaml);
            }
        }
    }
}