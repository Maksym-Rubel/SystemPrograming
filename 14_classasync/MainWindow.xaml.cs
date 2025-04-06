using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _14_classasync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            srcTextBox.Text = Source = @"D:\Test\1GB.bin";
            destTextBox.Text = Target = @"C:\Users\Maksym\Desktop\TestCopy";

        }

        private void OpenFileBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                srcTextBox.Text = Source = dialog.FileName;
            }
        }

        private void OpenFolderBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destTextBox.Text = Target = dialog.FileName;
            }
        }

        private async void CopyFile(object sender, RoutedEventArgs e)
        {

            string fileName = System.IO.Path.Combine(Target, System.IO.Path.GetFileName(srcTextBox.Text));
            //File.Copy(Source, Targert1);

            await CopyFileAsync(Source, fileName);
           
            MessageBox.Show("Complate");
        }
        private Task CopyFileAsync(string source, string dest)
        {
            return Task.Run(() =>
            {
                using (FileStream scrFile = new FileStream(source, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destFile = new FileStream(dest, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024 * 8];
                        int bytes = 0;
                        do
                        {
                            bytes = scrFile.Read(buffer, 0, buffer.Length);
                            destFile.Write(buffer, 0, bytes);
                            float percentage = destFile.Length / (scrFile.Length / 100);
                            Dispatcher.Invoke(() => { 
                                progress.Value = percentage;
                                percentage_.Text = $"{percentage}%";
                            });
                        
                           

                        } while (bytes > 0);
                    }
                }
            });
        }
    }
}