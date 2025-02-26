using System;
using System.Windows;
using System.Windows.Threading;

namespace NotesApp
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var mainWindow = new Views.MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup Error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Critical Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled Error: {e.Exception.Message}\n\nStack Trace:\n{e.Exception.StackTrace}", 
                "Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}