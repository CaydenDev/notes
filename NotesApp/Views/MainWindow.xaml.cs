using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NotesApp.ViewModels;
using NotesApp.Services;

namespace NotesApp.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new Services.NoteStorageService(), new Services.DialogService());
            Editor.TextChanged += Editor_TextChanged;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ViewModel.SelectedNote != null)
            {
                ViewModel.SelectedNote.RichContent = RichTextHelper.GetRichTextXaml(Editor);
                ViewModel.UpdateNoteCommand.Execute(ViewModel.SelectedNote);
            }
        }

        private void OnNoteSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.SelectedNote != null)
            {
                RichTextHelper.SetRichTextXaml(Editor, ViewModel.SelectedNote.RichContent);
            }
            else
            {
                Editor.Document.Blocks.Clear();
            }
        }

        private void FormatBold_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selection = Editor.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, 
                    selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold) 
                        ? FontWeights.Normal 
                        : FontWeights.Bold);
            }
        }

        private void FormatItalic_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selection = Editor.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                    selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic)
                        ? FontStyles.Normal
                        : FontStyles.Italic);
            }
        }

        private void FormatUnderline_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selection = Editor.Selection;
            if (!selection.IsEmpty)
            {
                var textDecorations = selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    textDecorations?.Contains(TextDecorations.Underline[0]) == true
                        ? null
                        : TextDecorations.Underline);
            }
        }

        private void FontSize_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is ComboBoxItem item && 
                double.TryParse(item.Content?.ToString(), out double fontSize))
            {
                Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }
    }
}